#region

using System;

#endregion

namespace WiPFlash.Exceptions
{
    public class FailureToLaunchException : WiPFlashException
    {
        public FailureToLaunchException(string message) : base(message)
        {
        }

        public FailureToLaunchException(Exception originator, string message) : base(originator, message)
        {
        }
    }
}
