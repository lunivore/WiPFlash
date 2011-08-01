using System;
using System.Text;
using System.Windows.Automation;

namespace WiPFlash.Framework
{
    public class ConditionDescriber : IDescribeConditions
    {
        public string Describe(Condition condition)
        {
            var builder = new StringBuilder();
            DescribeToBuilder(condition, builder);
            return builder.ToString();
        }

        private void DescribeToBuilder(Condition condition, StringBuilder builder)
        {
            if (condition is PropertyCondition)
            {
                Describe(condition as PropertyCondition, builder);
            }
            else if (condition is AndCondition)
            {
                DescribeSubconditions(((AndCondition)condition).GetConditions(), " and ", builder);
            }
            else if (condition is OrCondition)
            {
                DescribeSubconditions(((OrCondition)condition).GetConditions(), " or ", builder);
            }
            else if (condition is NotCondition)
            {
                builder.Append("not ");
                DescribeToBuilder(((NotCondition) condition).Condition, builder);
            }
            else if (condition == Condition.FalseCondition) {
                builder.Append("false");
            }
            else if (condition == Condition.TrueCondition)
            {
                builder.Append("true");
            }
            else
            {
                builder.Append(condition.ToString());
            }
        }

        private void DescribeSubconditions(Condition[] subConditions, string concatenator, StringBuilder builder)
        {
            builder.Append("(");
            for (int index = 0; index < subConditions.Length; index++)
            {
                DescribeToBuilder(subConditions[index], builder);
                if (index < subConditions.Length - 1)
                {
                    builder.Append(concatenator);
                }
            }
            builder.Append(")");
        }

        private void Describe(PropertyCondition condition, StringBuilder builder)
        {
            builder.Append(String.Format("{0}='{1}'", ToSimplePropertyName(condition.Property.ProgrammaticName), condition.Value));
        }

        private string ToSimplePropertyName(string name)
        {
            return name.Replace("AutomationElementIdentifiers.", string.Empty).Replace("Property", string.Empty);
        }
    }
}