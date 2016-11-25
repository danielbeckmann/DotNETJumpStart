using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DotNETJumpStart.Models;
using DotNETJumpStart.Models.Dtos;

namespace DotNETJumpStart.ApiControllers
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

            // Check if user is already existing
            var user = db.Users.FirstOrDefault(u => u.Identifier == userDto.Identifier);
            if (user == null)
            {
                // Map dto to user object
                var newUser = new User
                {
                    Identifier = userDto.Identifier,
                    Name = userDto.Name
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = userDto.Identifier }, userDto);
            }
            else
            {
                return Ok(user);
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