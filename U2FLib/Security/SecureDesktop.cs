using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace U2FLib.Security
{
    public class SecureDesktop : IDisposable
    {
        public enum DESKTOP_ACCESS : uint
        {
            DESKTOP_NONE = 0,
            DESKTOP_READOBJECTS = 0x0001,
            DESKTOP_CREATEWINDOW = 0x0002,
            DESKTOP_CREATEMENU = 0x0004,
            DESKTOP_HOOKCONTROL = 0x0008,
            DESKTOP_JOURNALRECORD = 0x0010,
            DESKTOP_JOURNALPLAYBACK = 0x0020,
            DESKTOP_ENUMERATE = 0x0040,
            DESKTOP_WRITEOBJECTS = 0x0080,
            DESKTOP_SWITCHDESKTOP = 0x0100,

            _FOR_SECURE_DESKTOP = DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
                                    DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP,

            GENERIC_ALL = DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
                            DESKTOP_HOOKCONTROL | DESKTOP_JOURNALRECORD | DESKTOP_JOURNALPLAYBACK |
                            DESKTOP_ENUMERATE | DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP,
        }

        private readonly string guid = Guid.NewGuid().ToString();
        private bool switched = false;
        private IntPtr Desktop;
        private IntPtr originalDesktop;

        public static ThreadOnSecureDesktop OnSecureDesktop(Action action)
        {
            using var secureDesktop = new SecureDesktop(DESKTOP_ACCESS._FOR_SECURE_DESKTOP);
            return secureDesktop.Run(action);
        }

        public SecureDesktop() : this(DESKTOP_ACCESS._FOR_SECURE_DESKTOP) { }

        public SecureDesktop(DESKTOP_ACCESS access)
        {
            Desktop = CreateDesktop(guid, IntPtr.Zero, IntPtr.Zero, 0, (uint)access, IntPtr.Zero);
        }

        public void Switch()
        {
            if (!switched)
            {
                originalDesktop = GetThreadDesktop(GetCurrentThreadId());
                switched = true;
            }
            SwitchDesktop(Desktop);
        }

        public ThreadOnSecureDesktop Run(Action action)
        {
            Switch();
            return RunThread(action);
        }

        public void Return()
        {
            if (!switched) return;
            SwitchDesktop(originalDesktop);
            switched = false;
            originalDesktop = IntPtr.Zero;
        }

        public void Close()
        {
            if (Desktop == IntPtr.Zero) return;

            Return();
            CloseDesktop(Desktop);
            Desktop = IntPtr.Zero;
        }

        private ThreadStart GetThreadStart(ThreadOnSecureDesktop t, Action action) => () =>
        {
            SetThreadDesktop(Desktop);
            action();
            t.SignalThreadDone();
        };

        private ThreadOnSecureDesktop RunThread(Action action)
        {
            var tOSD = new ThreadOnSecureDesktop();
            var threadStart = GetThreadStart(tOSD, action);
            var thread = new Thread(threadStart) { Name = $"SecureDesktop-{guid}" };
            tOSD.Thread = thread;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tOSD;
        }

        #region Imports

        [DllImport("user32.dll")]
        private static extern IntPtr GetThreadDesktop(int dwThreadId);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, uint dwDesiredAccess, IntPtr lpsa);
        [DllImport("user32.dll")]
        private static extern bool SwitchDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        private static extern bool SetThreadDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        private static extern bool CloseDesktop(IntPtr hDesktop);

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class ThreadOnSecureDesktop
    {
        public event EventHandler ThreadDone;

        public Thread Thread { get; internal protected set; }
        public void Join() => Thread.Join();

        public Task DoneTask()
        {
            var options = TaskCreationOptions.RunContinuationsAsynchronously;
            var tcs = new TaskCompletionSource(options);
            ThreadDone += onThreadDone;

            void onThreadDone(object sender, EventArgs e)
            {
                tcs.SetResult();
                ThreadDone -= onThreadDone;
            };

            return tcs.Task;
        }

        internal protected void SignalThreadDone() => ThreadDone?.Invoke(this, new());
    }
}
