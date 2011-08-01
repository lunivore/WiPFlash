#region

using System.Collections;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace Example.PetShop.AccessoryRegistry.View
{
    /**
     * With thanks to Marlon Grech for this little trick:
     * http://marlongrech.wordpress.com/2009/06/02/sync-multi-select-listbox-with-viewmodel/
     */
    public class ListBoxHelper
    {
        #region SelectedItems

        /// <summary>
        /// SelectedItems Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ListBoxHelper),
                                                new FrameworkPropertyMetadata((IList)null,
                                                                              new PropertyChangedCallback(OnSelectedItemsChanged)));

        /// <summary>
        /// Gets the SelectedItems property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static IList GetSelectedItems(DependencyObject d)
        {
            return (IList)d.GetValue(SelectedItemsProperty);
        }

        /// <summary>
        /// Sets the SelectedItems property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetSelectedItems(DependencyObject d, IList value)
        {
            d.SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Handles changes to the SelectedItems property.
        /// </summary>
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBox = (ListBox)d;
            ReSetSelectedItems(listBox);
            listBox.SelectionChanged += delegate
                                            {
                                                ReSetSelectedItems(listBox);
                                            };
        }

        #endregion

        private static void ReSetSelectedItems(ListBox listBox)
        {
            IList selectedItems = GetSelectedItems(listBox);
            selectedItems.Clear();
            if (listBox.SelectedItems != null)
            {
                foreach (var item in listBox.SelectedItems)
                    selectedItems.Add(item);
            }
        }
    }
}
