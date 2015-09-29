using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAdminAndApi.Models;
using WebAdminAndApi.Utils;

namespace WebAdminAndApi.Controllers
{
    public class PostsController : Controller
    {
        private ImageAppDbContext db = new ImageAppDbContext();
        private bool disposed = false;

        // GET: /Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            // Validate that a image was selected
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Ein Bild ist erforderlich");
            }

            if (ModelState.IsValid)
            {
                // Get image from request and save
                var image = ImageUtility.SaveImageFromRequest();

                // Save image to db
                image = db.Images.Add(image);
                db.SaveChanges();

                // Assign the image to the post
                post.Image = image;

                // Only the admin can post images here, so select admin
                post.User = db.Users.FirstOrDefault(x => x.Name == "Admin");

                // Save post to db
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Post/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: /Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            // Delete image from file system
            ImageUtility.DeleteImageFromDisk(post.Image);

            // Delete all referenced posts
            db.Likes.RemoveRange(post.Likes);

            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Post/Details/5
        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: /Post/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: /Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;

                // Update image
                post.Image = ImageUtility.GetImageAndUpdateOnDisk(post.Image);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Post/
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposed)
                {
                    db.Dispose();
                    disposed = true;
                }
            }
            base.Dispose(disposing);
        }
    }
}