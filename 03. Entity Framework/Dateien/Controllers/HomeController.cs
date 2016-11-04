using System.Linq;
using System.Web.Mvc;
using WebAdminAndApi.Models;

namespace WebAdminAndApi.Controllers
{
    public class HomeController : Controller
    {
        private ImageAppDbContext db = new ImageAppDbContext();
     
        public ActionResult Index()        
        {
            var posts = db.Posts.OrderByDescending(p => p.Created).Take(10);
            return View(posts.ToList());
        }
    }
}