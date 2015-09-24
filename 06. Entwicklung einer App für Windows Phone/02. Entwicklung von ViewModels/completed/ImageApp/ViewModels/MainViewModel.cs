using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.DataModel;

namespace ImageApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private List<Post> posts;

        public MainViewModel()
        {
            this.SortDateCommand = new DelegateCommand(this.SortByDate);
            this.SortRatingCommand = new DelegateCommand(this.SortByRating);
            this.LikeCommand = new DelegateCommand(this.Like);
            this.RefreshCommand = new DelegateCommand(this.RefreshData);
        }

        /// <summary>
        /// Gets or sets the list of posts.
        /// </summary>
        public List<Post> Posts
        {
            get { return this.posts; }
            set { this.SetProperty(ref this.posts, value); }
        }

        /// <summary>
        /// Gets or sets the command to sort after rating.
        /// </summary>
        public DelegateCommand SortRatingCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to sort after date.
        /// </summary>
        public DelegateCommand SortDateCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to like a post.
        /// </summary>
        public DelegateCommand LikeCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to refresh the posts.
        /// </summary>
        public DelegateCommand RefreshCommand { get; set; }

        private async void Like(object obj)
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Like command works!");
            await dialog.ShowAsync();
        }

        private async void SortByRating(object obj)
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Sort by rating command works!");
            await dialog.ShowAsync();
        }

        private async void SortByDate(object obj)
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Sort by date command works!");
            await dialog.ShowAsync();
        }

        private async void RefreshData(object obj)
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Refresh command works!");
            await dialog.ShowAsync();
        }
    }
}
