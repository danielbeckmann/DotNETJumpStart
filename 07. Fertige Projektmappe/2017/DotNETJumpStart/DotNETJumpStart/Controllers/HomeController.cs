using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNETJumpStart.Models;

namespace DotNETJumpStart.Controllers
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
