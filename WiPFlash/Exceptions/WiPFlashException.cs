#region

using System;

#endregion

namespace WiPFlash.Exceptions
{
    public class WiPFlashException : Exception
    {
        protected WiPFlashException(string message) : base(message)
        {
        }

        protected WiPFlashException(Exception originator, string message) : base(message, originator)
        {            
        }
    }
}