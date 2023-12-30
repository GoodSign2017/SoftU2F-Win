/**
  *
  * This file (and this file only) contains substantial portions of the work:
  *  https://github.com/sirAndros/KeePassWinHello
  *
  * and in addition to the Unlicense is licensed under:
  *
  *  The MIT License (MIT)
  *  
  *  Copyright (c) Andrei Osipkov, Alexander Sitnikov
  *  
  *  All rights reserved.
  *  
  *  Copyright (c) GoodSign2017 ( https://github.com/GoodSign2017 )
  *  
  *  Permission is hereby granted, free of charge, to any person obtaining a copy
  *  of this software and associated documentation files (the "Software"), to deal
  *  in the Software without restriction, including without limitation the rights
  *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  *  copies of the Software, and to permit persons to whom the Software is
  *  furnished to do so, subject to the following conditions:
  *  
  *  The above copyright notice and this permission notice shall be included in all
  *  copies or substantial portions of the Software.
  *  
  *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  *  SOFTWARE.
  *
  */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace U2FLib.Security
{
    public class MicrosoftPassportHelper
    {
        private const string MICROSOFT_PASSPORT_KEY_STORAGE_PROVIDER = "Microsoft Passport Key Storage Provider";
        private static readonly CngProvider KEY_PROVIDER = new(MICROSOFT_PASSPORT_KEY_STORAGE_PROVIDER);

        // Exceptions:
        //   T:System.ArgumentException:
        //     key is not a valid RSA key.
        //
        //   T:System.ArgumentNullException:
        //     key or keyName or provider is null.
        //
        //   T:System.PlatformNotSupportedException:
        //     Cryptography Next Generation (CNG) is not supported on this system.
        //
        //   T:System.Security.Cryptography.CryptographicException:
        //     All other errors.
        public static RSA RSAKey(MPHelperParameters param = null)
        {
            param ??= new();

            var key = CheckCngKey(param.KeyNameFull)
                ? OpenCngKey(param)
                : CreateCngKey(param);

            return new RSACng(key);
        }

        private static bool CheckCngKey(string keyname)
        {
            try
            {
                return CngKey.Exists(keyname, KEY_PROVIDER);
            }
            catch (CryptographicException e)
            {
                if (e.Message == "Key does not exist.") return false;
                throw;
            }
        }

        private static CngKey OpenCngKey(MPHelperParameters param)
        {
            var key = CngKey.Open(param.KeyNameFull, KEY_PROVIDER);
            var propertySetter = new CngPropertySetter(key.SetProperty);

            if (param.HasWindowHandle) propertySetter.WindowContextProperty(param.WindowHandle);
            if (param.HasUseContext) propertySetter.UseContextProperty(param.UseContext);
            if (param.GestureRequired) propertySetter.GestureRequirementProperty();

            return key;
        }

        private static CngKey CreateCngKey(MPHelperParameters param)
        {
            var creationParameters = new CngKeyCreationParameters { Provider = KEY_PROVIDER };
            var propertySetter = new CngPropertySetter(creationParameters.Parameters.Add);

            propertySetter.LengthProperty();
            propertySetter.KeyUsageProperty();
            propertySetter.CacheTypeProperty();
            if (param.HasWindowHandle) propertySetter.WindowContextProperty(param.WindowHandle);
            if (param.HasUseContext) propertySetter.UseContextProperty(param.UseContext);

            return CngKey.Create(CngAlgorithm.Rsa, param.KeyNameFull, creationParameters);
        }
    }

    public class CngPropertySetter
    {
        private const int KEY_LENGTH = 2048;

        private const int NCRYPT_ALLOW_DECRYPT_FLAG = 0x00000001;
        private const int NCRYPT_ALLOW_SIGNING_FLAG = 0x00000002;
        private const int NCRYPT_NGC_CACHE_TYPE_PROPERTY_AUTH_MANDATORY_FLAG = 0x00000001;

        private const string NCRYPT_KEY_USAGE_PROPERTY = "Key Usage";
        private const string NCRYPT_LENGTH_PROPERTY = "Length";
        private const string NCRYPT_NGC_CACHE_TYPE_PROPERTY = "NgcCacheType";
        private const string NCRYPT_NGC_CACHE_TYPE_PROPERTY_DEPRECATED = "NgcCacheTypeProperty";
        private const string NCRYPT_PIN_CACHE_IS_GESTURE_REQUIRED_PROPERTY = "PinCacheIsGestureRequired";
        private const string NCRYPT_USE_CONTEXT_PROPERTY = "Use Context";
        private const string NCRYPT_WINDOW_HANDLE_PROPERTY = "HWND Handle";

        private readonly Action<CngProperty> propertyConsumer;

        public CngPropertySetter(Action<CngProperty> propertyConsumer)
        {
            this.propertyConsumer = propertyConsumer;
        }

        public void LengthProperty()
        {
            Set(NCRYPT_LENGTH_PROPERTY, BitConverter.GetBytes(KEY_LENGTH));
        }

        public void KeyUsageProperty()
        {
            Set(NCRYPT_KEY_USAGE_PROPERTY, BitConverter.GetBytes(NCRYPT_ALLOW_DECRYPT_FLAG | NCRYPT_ALLOW_SIGNING_FLAG));
        }

        //
        // This would break on Windows 10 1803 and I didn't have figured out the fix using managed CNG
        // in KeePassWinHello the fix was with try-catch https://github.com/sirAndros/KeePassWinHello/issues/33
        // but in managed CNG the exception is not raised when the property is set, only on CngKey.Create
        //
        // Consider to find some workaround if there would be much demand, but people better update their Windows.
        // This property is very important, as it telling not to cache confirmation (PIN/password/biometry..).
        // If this property is not set on key creation, then another app may bypass confrimation.
        // 
        // On Windows 10 1803 NCRYPT_NGC_CACHE_TYPE_PROPERTY_DEPRECATED must be used instead
        // - GoodSign2017
        public void CacheTypeProperty()
        {
            var authMandatoryFlag = BitConverter.GetBytes(NCRYPT_NGC_CACHE_TYPE_PROPERTY_AUTH_MANDATORY_FLAG);
            Set(NCRYPT_NGC_CACHE_TYPE_PROPERTY, authMandatoryFlag);
        }

        public void CacheTypePropertyDeprecated()
        {
            var authMandatoryFlag = BitConverter.GetBytes(NCRYPT_NGC_CACHE_TYPE_PROPERTY_AUTH_MANDATORY_FLAG);
            Set(NCRYPT_NGC_CACHE_TYPE_PROPERTY_DEPRECATED, authMandatoryFlag);
        }

        public void GestureRequirementProperty()
        {
            Set(NCRYPT_PIN_CACHE_IS_GESTURE_REQUIRED_PROPERTY, BitConverter.GetBytes(1));
        }

        public void WindowContextProperty(IntPtr wHandle)
        {
            Set(NCRYPT_WINDOW_HANDLE_PROPERTY, BitConverter.GetBytes(IntPtrToLong(wHandle)));
        }

        public void UseContextProperty(string useContext)
        {
            Set(NCRYPT_USE_CONTEXT_PROPERTY, Encoding.Unicode.GetBytes($"{useContext}\0"));
        }

        public void Set(string name, byte[] value) => propertyConsumer(new(name, value, CngPropertyOptions.None));

        private static long IntPtrToLong(IntPtr wHandle) => IntPtr.Size == 8 ? wHandle.ToInt64() : wHandle.ToInt32();
    }

    public class MPHelperParameters
    {
        public bool GestureRequired = true;

        public string UseContext;
        public IntPtr WindowHandle;
        public string KeyNameDomain = typeof(MicrosoftPassportHelper).Namespace;
        public string KeyNameSubDomain = typeof(MicrosoftPassportHelper).Name;
        public string KeyName = "Default";

        private readonly string sid = WindowsIdentity.GetCurrent().User?.Value;
        public bool HasUseContext => !string.IsNullOrEmpty(UseContext);
        public bool HasWindowHandle => WindowHandle != IntPtr.Zero;
        public string KeyNameFull => $"{sid}//{KeyNameDomain}/{KeyNameSubDomain}/{KeyName}";
    }
}