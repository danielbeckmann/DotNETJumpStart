using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DotNETJumpStart.Models;
using DotNETJumpStart.Models.Dtos;

namespace DotNETJumpStart.ApiControllers
{
    public class LikesController : ApiController
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        // POST: api/likes
        [ResponseType(typeof(Like))]
        public IHttpActionResult PostLike(LikeDto likeDto)
        {
            // Get or create user
            var user = db.Users.FirstOrDefault(u => u.Identifier == likeDto.UserIdentifier);
            if (user == null)
            {
                user = new User
                {
                    Identifier = likeDto.UserIdentifier
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            // Get post
            var post = db.Posts.Find(likeDto.PostId);
            if (post == null)
            {
                return BadRequest("Invalid post");
            }

            var like = db.Likes.FirstOrDefault(l => l.User.Identifier == likeDto.UserIdentifier && l.Post.Id == likeDto.PostId);

            if (like != null)
            {
                // Remove the like and return
                db.Likes.Remove(like);

                db.SaveChanges();
                return Ok();
            }
            else
            {
                like = new Like
                {
                    User = user,
                    Post = post
                };

                // Add like
                db.Likes.Add(like);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = like.Id }, likeDto);
            }
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