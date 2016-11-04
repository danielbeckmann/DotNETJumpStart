using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAdminAndApi.Utils;

namespace WebAdminAndApi.Models.Dtos
{
    /// <summary>
    /// Data transfer object for post objects.
    /// </summary>
    public class PostDto
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string Title { get; set; }

        public string ImageUri { get; set; }

        public int Likes { get; set; }

        public DateTime Created { get; set; }

        public static PostDto Map(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                ImageUri = ImageUtility.GetFileUri(post.Image.FileName),
                User = post.User.Name,
                Likes = post.Likes.Count,
                Created = post.Created
            };
        }
    }
}