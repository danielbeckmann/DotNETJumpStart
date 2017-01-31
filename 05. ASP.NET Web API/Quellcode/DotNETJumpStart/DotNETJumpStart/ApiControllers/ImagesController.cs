using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DotNETJumpStart.Models;
using DotNETJumpStart.Utils;

namespace DotNETJumpStart.ApiControllers
{
    public class ImagesController : ApiController
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        // POST api/images
        [ResponseType(typeof(Image))]
        public IHttpActionResult Post()
        {
            // Check request type
            if (!this.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Get image from request and save
            var image = ImageUtility.SaveImageFromRequest();

            // Save image to db
            image = db.Images.Add(image);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = image.Id }, image);
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