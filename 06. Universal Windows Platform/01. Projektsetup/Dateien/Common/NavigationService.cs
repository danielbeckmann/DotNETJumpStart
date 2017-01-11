using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ImageApp.Common
{
    /// <summary>
    /// Service for in-app-navigation.
    /// </summary>
    public class NavigationService
    {
        // Singleton instance
        public static NavigationService Current = new NavigationService();

        private NavigationService()
        {
        }

        /// <summary>
        /// Navigates to a certain page.
        /// </summary>
        /// <param name="sourcePage">The type of the page</param>
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        /// <summary>
        /// Navigates to a certain page with a parameter.
        /// </summary>
        /// <param name="sourcePage">The type of the page</param>
        /// <param name="parameter">The navigation paraneter</param>
        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        /// <summary>
        /// Navigates a frame back if possible.
        /// </summary>
        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}
