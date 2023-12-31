using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Zxcvbn;
using ZxcvbnCore = Zxcvbn.Core;

namespace SoftU2FDaemon.PasswordProtection
{
    class StrengthEstimation
    {
        internal static readonly PasswordStrength PASS_STRENGTH_NONE = new("Score", Color.LightGray, Color.DimGray);
        internal static readonly PasswordStrength PASS_STRENGTH_WEAK = new("Weak", Color.OrangeRed, Color.Black);
        internal static readonly PasswordStrength PASS_STRENGTH_COMMON = new("Common", Color.Orange, Color.Black);
        internal static readonly PasswordStrength PASS_STRENGTH_OKAY = new("Average", Color.Yellow, Color.Black);
        internal static readonly PasswordStrength PASS_STRENGTH_GOOD = new("Good", Color.YellowGreen, Color.DarkBlue);
        internal static readonly PasswordStrength PASS_STRENGTH_STRONG = new("Strong", Color.LightGreen, Color.Blue);

        internal static readonly PasswordStrength[] RESULT_PASS_STRENGTHS = {
            PASS_STRENGTH_WEAK,
            PASS_STRENGTH_COMMON,
            PASS_STRENGTH_OKAY,
            PASS_STRENGTH_GOOD,
            PASS_STRENGTH_STRONG
        };

        private bool estimated;
        private bool goodEnough;
        private int? goodEnoughFrom;
        private PasswordStrength currentPasswordStrength;
        private StringBuilder zxcvbnOut;
        private readonly IEstimatedOn componentAccess;
        private readonly Func<SecureString> secretAccess;

        private SecureString Secret => secretAccess();

        private bool _estimateStrength = false;

        public bool EstimateStrength
        {
            get => _estimateStrength;
            set { _estimateStrength = value; InitEstimation(); }
        }

        public StrengthEstimation(IEstimatedOn componentAccess, Func<SecureString> secretAccess)
        {
            this.componentAccess = componentAccess;
            this.secretAccess = secretAccess;
        }

        public void InitEstimation()
        {
            ResetEstimation();
            PerformEstimation();
        }

        private void ResetEstimation()
        {
            estimated = false;
            goodEnough = false;
            goodEnoughFrom = null;

            if (EstimateStrength)
            {
                componentAccess.EstimateTick += EstimateTimer_Tick;
                componentAccess.ChangedAtEvent += OnChangedAtEvent;
            }
            else
            {
                componentAccess.EstimateTick -= EstimateTimer_Tick;
                componentAccess.ChangedAtEvent -= OnChangedAtEvent;
            }

            currentPasswordStrength = PASS_STRENGTH_NONE;
            zxcvbnOut = EstimateStrength ? new StringBuilder() : null;
            componentAccess.EstimationVisible = EstimateStrength;
            componentAccess.ScoreVisible = EstimateStrength;
        }

        private void OnChangedAtEvent(object sender, ChangedAtEventArgs e)
        {
            if (!goodEnough || e.ChangedStart < goodEnoughFrom)
            {
                estimated = false;
                goodEnough = false;
                goodEnoughFrom = null;
            }
        }

        private void EstimateTimer_Tick(object sender, EventArgs e) => PerformEstimation();

        private void PerformEstimation()
        {
            if (!EstimateStrength || estimated) return;

            if (Secret.Length > 0)
            {
                componentAccess.EstimationText = "Estimating strength of the password...";
                EstimateEvaluateWithZxcvbn();
            }
            else
            {
                currentPasswordStrength = PASS_STRENGTH_NONE;
                componentAccess.EstimationText = "The password will be estimated as you type";
            }

            UpdateScoreIndicator();
            estimated = true;
        }

        private void EstimateEvaluateWithZxcvbn()
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(Secret);
                var unsecuredPwd = Marshal.PtrToStringUni(valuePtr);
                var result = ZxcvbnCore.EvaluatePassword(unsecuredPwd);
                var passwordStrength = GetPasswordStrengthByResult(result);
                goodEnough = IsGoodEnoughPassword(passwordStrength, result);
                goodEnoughFrom = goodEnough ? Secret.Length : null;
                currentPasswordStrength = passwordStrength;
                componentAccess.EstimationText = GetEstimationResultText(result);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                GC.Collect();
            }
        }

        private string GetEstimationResultText(Result result)
        {
            zxcvbnOut.Clear().AppendLine("Password estimation by :");
            if (result.Feedback.Warning.Length > 0)
            {
                zxcvbnOut.AppendLine($"Warning! {result.Feedback.Warning}");
            }

            var suggestions = goodEnough
                ? $"Good password! Extra length could be added to increase strength (estimation stopped)"
                : $"Suggestions: {String.Join(" ", result.Feedback.Suggestions)}";

            zxcvbnOut.AppendJoin("\r\n", new string[] {
                    suggestions,
                    $"Average guesses to crack: {result.Guesses}",
                    "",
                    $"Approx times to crack ...",
                    $"   100/hour: {result.CrackTimeDisplay.OnlineThrottling100PerHour}",
                    $"  10/second: {result.CrackTimeDisplay.OnlineNoThrottling10PerSecond}",
                    $" 10k/second: {result.CrackTimeDisplay.OfflineSlowHashing1e4PerSecond}",
                    $" 10B/second: {result.CrackTimeDisplay.OfflineFastHashing1e10PerSecond}",
                });

            return zxcvbnOut.ToString();
        }

        private static bool IsGoodEnoughPassword(PasswordStrength passwordStrength, Result result)
        {
            return passwordStrength == PASS_STRENGTH_STRONG
                && result.CrackTimeDisplay.OfflineFastHashing1e10PerSecond == "centuries";
        }

        private static PasswordStrength GetPasswordStrengthByResult(Result result)
        {
            var score = result.Score;
            var passwordStrength = RESULT_PASS_STRENGTHS[score];
            return passwordStrength;
        }

        private void UpdateScoreIndicator()
        {
            componentAccess.ScoreText = currentPasswordStrength.Text;
            componentAccess.ScoreForeColor = currentPasswordStrength.ForeColor;
            componentAccess.ScoreBackColor = currentPasswordStrength.BackColor;
        }
    }

    public interface IEstimatedOn
    {
        public bool EstimationVisible { get; set; }
        public string EstimationText { get; set; }
        public bool ScoreVisible { get; set; }
        public Color ScoreForeColor { get; set; }
        public Color ScoreBackColor { get; set; }
        public string ScoreText { get; set; }

        public event EventHandler<ChangedAtEventArgs> ChangedAtEvent;

        public event EventHandler EstimateTick;
    }

    public class ChangedAtEventArgs : EventArgs
    {
        public int ChangedStart { get; set; }
    }

    public class PasswordStrength
    {
        public readonly string Text;
        public readonly Color BackColor;
        public readonly Color ForeColor;

        public PasswordStrength(string text, Color backColor, Color foreColor)
        {
            Text = text;
            BackColor = backColor;
            ForeColor = foreColor;
        }
    }
}
