using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiPFlash.Exceptions
{
    public class FailureToSetTextException : WiPFlashException
    {
        public FailureToSetTextException(string message) : base(message)
        {
        }

        public FailureToSetTextException(Exception originator, string message) : base(originator, message)
        {
        }
    }
}
