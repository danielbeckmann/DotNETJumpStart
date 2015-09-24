using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebAdminAndApi.Models;
using WebAdminAndApi.Models.Dtos;
using WorkshopMVC.Models;

namespace WebAdminAndApi.ApiControllers
{
    public class UsersController : ApiController
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        // GET: api/users/{identifier}
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult GetUser(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.Identifier == id);
            if (user == null)
            {
                return NotFound();
            }

            // Map found user to dto
            var result = UserDto.Map(user);
            return Ok(result);
        }

        // POST: api/users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map dto to user object
            var user = new User
            {
                Identifier = userDto.Identifier,
                Name = userDto.Name
            };

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userDto.Identifier }, userDto);
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