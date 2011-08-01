using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.PetShop.Domain
{
    public class PaymentMethod
    {
        private readonly string _method;
        private readonly bool _isValid;

        private PaymentMethod(string method) : this(method, true){}

        private PaymentMethod(string method, bool isValid)
        {
            _method = method;
            _isValid = isValid;
        }

        public bool IsValid
        {
            get { return _isValid; }
        }

        public override string ToString()
        {
            return string.Format("PaymentMethod[{0}]", _method);
        }

        public static readonly PaymentMethod Cash = new PaymentMethod("Cash");
        public static readonly PaymentMethod Card = new PaymentMethod("Card");
        public static readonly PaymentMethod Cheque = new PaymentMethod("Cheque");
        public static readonly PaymentMethod NoPaymentMethodSelected = new PaymentMethod("None", false);

        
    }
}
