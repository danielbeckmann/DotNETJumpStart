using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ImageApp.Common
{
    /// <summary>
    /// Converts a list to a Visibility state. If the list has entries, the value is Visible.
    /// </summary>
    public class CollectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a list to a Visibility state. If the list has entries, the value is Visible.
        /// </summary>
        /// <param name="value">The collection which gets converted</param>
        /// <param name="targetType">Type: Visibility</param>
        /// <param name="parameter">Provide any parameter to invert the visibility state</param>
        /// <param name="language">The Language</param>
        /// <returns>Visibility</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible = false;

            if (value is ICollection)
            {
                visible = ((ICollection)value).Count > 0;
            }

            if (parameter != null)
            {
                visible = !visible;
            }

            if (visible)
            {
                return Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
