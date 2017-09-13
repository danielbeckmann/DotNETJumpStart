using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNETJumpStart.Utils;

namespace DotNETJumpStart.Models.Dtos
{
    /// <summary>
    /// Data transfer object for post objects.
    /// </summary>
    public class PostDto
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string UserIdentifier { get; set; }

        public string Title { get; set; }

        public int ImageId { get; set; }

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
                ImageId = post.Image.Id,
                User = post.User.Name,
                UserIdentifier = post.User.Identifier,
                Likes = post.Likes.Count,
                Created = post.Created
            };
        }
    }
}