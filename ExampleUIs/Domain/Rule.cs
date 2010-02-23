#region

using System.Collections.Generic;

#endregion

namespace ExampleUIs.PetModule.Domain
{
    public class Rule
    {
        public static Rule SELL_IN_PAIRS = new Rule(
            "Sell In Pairs", 
            "This animal requires company. It must be sold in pairs or to a household which already has one.");
        public static Rule CHECK_ENVIRONMENT = new Rule(
            "Special Environment", 
            "This animal requires a specific environment - please check with customer.");
        public static Rule UNSUITABLE_FOR_CHILDREN = new Rule(
            "No Children", 
            "This animal is not suitable for a household with children - please check with customer.");
        public static Rule DANGEROUS = new Rule(
            "Dangerous", 
            "This animal can be dangerous. Provide customer with appropriate advice before sale");

        public static List<Rule> ALL = new List<Rule>{SELL_IN_PAIRS, CHECK_ENVIRONMENT, UNSUITABLE_FOR_CHILDREN, DANGEROUS};

        private readonly string _text;
        private readonly string _name;

        private Rule(string name, string text)
        {
            _text = text;
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Text
        {
            get { return _text; }
        }

        public override string ToString()
        {
            return "Rule[" + Name + "]";
        }
    }
}