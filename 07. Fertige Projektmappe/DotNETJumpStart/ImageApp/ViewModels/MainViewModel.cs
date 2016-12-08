using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.DataModel;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using ImageApp.Utils;

namespace ImageApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public enum Sorting { Latest, Popular };

        private Sorting currentSorting = Sorting.Latest;

        private List<Post> posts;

        public MainViewModel()
        {
            this.posts = new List<Post>();
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

        /// <summary>
        /// Gets the posts from the api.
        /// </summary>
        /// <param name="sorting">Current sorting</param>
        public async Task GetPostsAsync(Sorting sorting = Sorting.Latest)
        {
            this.currentSorting = sorting;

            using (var client = new RestClient("http://localhost:55298/api/"))
            {
                var request = new RestRequest("posts/{sorting}", Method.GET);
                request.AddUrlSegment("sorting", sorting.ToString());

                var result = await client.Execute<List<Post>>(request);

                this.Posts = result.Data;
            }
        }

        /// <summary>
        /// Toggles the like of the current post on the api.
        /// </summary>
        public async Task LikePostAsync(Post post)
        {
            var like = new Like
            {
                PostId = post.Id,
                UserIdentifier = DeviceUtils.DeviceId
            };

            using (var client = new RestClient("http://localhost:55298/api/"))
            {
                var request = new RestRequest("likes", Method.POST);
                request.AddBody(like);

                var result = await client.Execute(request);

                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    post.Likes++;
                }
                else
                {
                    post.Likes--;
                }
            }
        }

        private async void Like(object obj)
        {
            var post = obj as Post;
            if (post != null)
            {
                await this.LikePostAsync(post);
            }
        }

        private async void SortByRating(object obj)
        {
            await this.GetPostsAsync(Sorting.Popular);
        }

        private async void SortByDate(object obj)
        {
            await this.GetPostsAsync(Sorting.Latest);
        }

        private async void RefreshData(object obj)
        {
            await this.GetPostsAsync();
        }
    }
}
