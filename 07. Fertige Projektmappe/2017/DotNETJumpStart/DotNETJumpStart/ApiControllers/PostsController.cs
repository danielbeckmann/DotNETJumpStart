using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DotNETJumpStart.Models;
using DotNETJumpStart.Models.Dtos;
using DotNETJumpStart.Utils;

namespace DotNETJumpStart.ApiControllers
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

        // GET api/posts/popular
        [Route("api/posts/popular")]
        public IEnumerable<PostDto> GetPopular()
        {
            return this.db.Posts.OrderByDescending(o => o.Likes.Count).Take(10).ToList().Select(p => PostDto.Map(p));
        }

        // POST: api/posts
        [ResponseType(typeof(PostDto))]
        public IHttpActionResult PostPost(PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get or create user
            var user = db.Users.FirstOrDefault(u => u.Identifier == postDto.UserIdentifier);
            if (user == null)
            {
                user = new User
                {
                    Identifier = postDto.UserIdentifier
                };

                db.Users.Add(user);
                db.SaveChanges();
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