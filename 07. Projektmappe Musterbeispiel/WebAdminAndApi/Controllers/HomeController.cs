using System.Linq;
using System.Web.Mvc;
using WebAdminAndApi.Models;

namespace WebAdminAndApi.Controllers
{
    public class HomeController : Controller
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Index()        
        {
            var posts = db.Posts.OrderByDescending(p => p.Created).Take(10);
            return View(posts.ToList());
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