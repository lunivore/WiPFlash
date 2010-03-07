#region

using System;

#endregion

namespace WiPFlash.Exceptions
{
    public class FailureToHappenException : Exception
    {
        public FailureToHappenException(string message) : base(message)
        {            
        }

        public FailureToHappenException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}