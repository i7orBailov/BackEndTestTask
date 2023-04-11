using BackEndTestTask.Models;

namespace BackEndTestTask.Helpers
{
    public class ErrorHelper
    {
        public const string incorrectInputParameters = "Error: incorrect input parameters";
        private static readonly object locker = new();

        public static void ThrowIfIncorrectInputParameters(params string[] inputParameters)
        {
            lock (locker) 
            {
                foreach (var parameter in inputParameters)
                {
                    if (string.IsNullOrWhiteSpace(parameter))
                    {
                        throw new SecureException(incorrectInputParameters + nameof(parameter));
                    }
                }
            }
        }

        public static void ThrowIfIncorrectInputParameters(params int[] inputParameters)
        {
            lock (locker)
            {
                foreach (var parameter in inputParameters)
                {
                    if (parameter <= 0)
                    {
                        throw new SecureException(incorrectInputParameters + nameof(parameter));
                    }
                }
            }
        }
    }
}
