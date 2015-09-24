using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp.Portable;
using ImageApp.DataModel;
using ImageApp.ViewModels;

namespace ImageApp.Services
{
    /// <summary>
    /// A service for all actions on the post entity.
    /// </summary>
    public class PostService
    {
        /// <summary>
        /// Adds a post to the api.
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public async Task<bool> AddPostAsync(int imageId, string title)
        {
            using (var client = new RestClient(Config.ApiAddress))
            {
                var request = new RestRequest("posts", HttpMethod.Post);

                var addPost = new AddPostRequest { ImageId = imageId, Title = title, UserIdentifier = SessionService.DeviceId };
                request.AddJsonBody(addPost);

                try
                {
                    await client.Execute(request);
                    return true;
                }
                catch
                {
                    return false;
                }
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
                UserIdentifier = SessionService.DeviceId
            };

            using (var client = new RestClient(Config.ApiAddress))
            {
                var request = new RestRequest("likes", HttpMethod.Post);
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

        /// <summary>
        /// Gets the posts from the api.
        /// </summary>
        /// <param name="sorting">Current sorting</param>
        public async Task<List<Post>> GetPostsAsync(MainViewModel.Sorting sorting = MainViewModel.Sorting.Latest)
        {
            using (var client = new RestClient(Config.ApiAddress))
            {
                var request = new RestRequest("posts/{sorting}", HttpMethod.Get);
                request.AddUrlSegment("sorting", sorting.ToString());
                var result = await client.Execute<List<Post>>(request);

                return result.Data;
            }
        }
    }
}
