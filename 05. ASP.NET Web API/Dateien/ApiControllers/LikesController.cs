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
        public IHttpActionResult PostLike(LikeDto likeDto)
        {
			return null;
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