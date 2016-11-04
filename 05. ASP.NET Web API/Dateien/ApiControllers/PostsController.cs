using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebAdminAndApi.Models;
using WebAdminAndApi.Models.Dtos;
using WebAdminAndApi.Utils;

namespace WebAdminAndApi.ApiControllers
{
    public class PostsController : ApiController
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        // GET api/posts
        public IEnumerable<PostDto> Get()
        {
            return this.db.Posts.ToList().Select(p => PostDto.Map(p));
        }

        // GET api/posts/latest
        [Route("api/posts/latest")]
        public IEnumerable<PostDto> GetLatest()
        {
            return this.db.Posts.OrderByDescending(o => o.Created).Take(10).ToList().Select(p => PostDto.Map(p));
        }

        // POST: api/posts
        [ResponseType(typeof(AddPostDto))]
        public IHttpActionResult PostPost(AddPostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find related user
            var user = db.Users.FirstOrDefault(u => u.Identifier == postDto.UserIdentifier);
            if (user == null)
            {
                return BadRequest("Invalid user");
            }

            // Find related image
            var image = db.Images.Find(postDto.ImageId);
            if (image == null)
            {
                return BadRequest("Image was not found");
            }

            // Map dto to post
            var post = new Post
            {
                Title = postDto.Title,
                Image = image,
                User = user
            };

            // Add to db
            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, postDto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    db.Dispose();
                    disposed = true;
                }
            }

            base.Dispose(disposing);
        }
    }
}