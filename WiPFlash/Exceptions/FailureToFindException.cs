#region

using System;

#endregion

namespace WiPFlash.Exceptions
{
    public class FailureToFindException : WiPFlashException
    {
        public FailureToFindException(string message) : base(message)
        {
        }

        public FailureToFindException(Exception originator, string message) : base(originator, message)
        {
        }
    }
}
