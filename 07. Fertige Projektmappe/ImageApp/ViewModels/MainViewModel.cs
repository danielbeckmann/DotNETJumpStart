using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageApp.Common;
using ImageApp.DataModel;
using ImageApp.Services;
using RestSharp.Portable;

namespace ImageApp.ViewModels
{
    /// <summary>
    /// Viewmodel for the main page.
    /// </summary>
    public class MainViewModel : BindableBase
    {
        public enum Sorting { Latest, Popular };
        private Sorting currentSorting = Sorting.Latest;

        private List<Post> posts;
        private int postIndex;

        private PostService postService;

        public MainViewModel()
        {
            this.postService = new PostService();

            this.posts = new List<Post>();
            this.SortDateCommand = new DelegateCommand(this.SortByDate);
            this.SortRatingCommand = new DelegateCommand(this.SortByRating);
            this.LikeCommand = new DelegateCommand(this.Like);
            this.RefreshCommand = new DelegateCommand(this.RefreshData);
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

        /// <summary>
        /// Gets or sets the list of posts.
        /// </summary>
        public List<Post> Posts
        {
            get { return this.posts; }
            set { this.SetProperty(ref this.posts, value); }
        }

        private Post currentPost;

        /// <summary>
        /// Gets or sets the current post.
        /// </summary>
        public Post CurrentPost
        {
            get { return currentPost; }
            set { this.SetProperty(ref this.currentPost, value); }
        }

        /// <summary>
        /// Gets or sets the current posts index.
        /// </summary>
        public int PostIndex
        {
            get { return postIndex; }
            set { this.SetProperty(ref this.postIndex, value); }
        }

        private async void Like(object obj)
        {
            await this.postService.LikePostAsync(this.currentPost);
        }

        private async void SortByRating(object obj)
        {
            this.currentSorting = Sorting.Popular;
            await this.GetPostsAsync();
        }

        private async void SortByDate(object obj)
        {
            this.currentSorting = Sorting.Latest;
            await this.GetPostsAsync();
        }

        private async void RefreshData(object obj)
        {
            await this.GetPostsAsync();
        }

        public async Task GetPostsAsync()
        {
            this.Posts = await this.postService.GetPostsAsync(this.currentSorting);
        }
    }
}
