#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using WiPFlash.Framework;
using WiPFlash.Framework.Events;
using WiPFlash.Framework.Patterns;

#endregion

namespace WiPFlash.Components
{
    public class GridView : AutomationElementWrapper<GridView>
    {
        private readonly TablePatternWrapper _tablePattern;
        private readonly IMatchConditions _conditionMatcher;
        private readonly IWrapAutomationElements _wrapperFactory;

        public GridView(AutomationElement element, string name) : this(element, name, new ConditionMatcher(), new WrapperFactory())
        {
            
        }

        public GridView(AutomationElement element, string name, IMatchConditions conditionMatcher, IWrapAutomationElements wrapperFactory) : base(element, name)
        {
            _conditionMatcher = conditionMatcher;
            _wrapperFactory = wrapperFactory;
            _tablePattern = new TablePatternWrapper(element);
        }

        protected override IEnumerable<AutomationEventWrapper> SensibleEventsToWaitFor
        {
            get
            {
                return new AutomationEventWrapper[]
                   {
                       new StructureChangeEvent(TreeScope.Element),
                       new PropertyChangeEvent(TreeScope.Descendants, AutomationElement.NameProperty)
                   };
            }
        }

        public string[,] AllText
        {
            get { return _tablePattern.AllText; }
        }

        public string TextAt(int column, int row)
        {
            return _tablePattern.TextAt(column, row);
        }

        public bool ContainsRow(params string[] fields)
        {
            var allText = AllText;
            if (allText.GetLength(1) != fields.Length) { return false; }
            var found = false;
            for (int row = 0; row < _tablePattern.RowCount && !found; row++)
            {
                found = true;
                for (int col = 0; col < _tablePattern.ColumnCount && found; col++)
                {
                    if (allText[row, col] != fields[col])
                    {
                        found = false;
                    }
                }
            }
            return found;
        }

        public int IndexOf(string header, Condition condition)
        {
            int columnWithHeader = _tablePattern.ColumnWithHeader(header);
            for(int row = 0; row < _tablePattern.RowCount; row++)
            {
                var element = _tablePattern.ElementAt(row, columnWithHeader);
                if (_conditionMatcher.Matches(element, condition))
                {
                    return row;
                }
            }
            return -1;
        }

        public bool ContainsRow(string header, PropertyCondition condition)
        {
            return IndexOf(header, condition) > -1;
        }

        /**
         * Given a row for which the cell in the referenced column matches the property condition,
         * returns the element in that row from the cell referenced by the target header.
         * 
         * eg: If a table has headers "Name" and "Price", and the values in a row are
         * "Dancer" and "54.00", the arguments would be "Name", Find.ByText("Dancer"), "Price" -
         * this would return the label which displays the 54.00 price.
         */
        public T FindReferencedElement<T>(string referenceHeader, PropertyCondition cellMatcherToFindRow, string targetHeader) where T : AutomationElementWrapper<T>
        {
            int referenceColumn = _tablePattern.ColumnWithHeader(referenceHeader);
            int targetColumn = _tablePattern.ColumnWithHeader(targetHeader);

            for (int row = 0; row < _tablePattern.RowCount; row++)
            {
                var element = _tablePattern.ElementAt(row, referenceColumn);
                if (_conditionMatcher.Matches(element, cellMatcherToFindRow))
                {
                    return _wrapperFactory.Wrap<T>(_tablePattern.ElementAt(row, targetColumn), cellMatcherToFindRow);
                }
            }
            return null;
        }

        public ListView AsListView()
        {
            return new ListView(Element, Name);
        }
    }
}
