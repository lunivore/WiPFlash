using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
