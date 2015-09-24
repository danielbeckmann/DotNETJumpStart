using ImageApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ImageApp
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.viewModel = this.DataContext as MainViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.viewModel.GetPostsAsync();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddPostPage));
        }
    }
}
