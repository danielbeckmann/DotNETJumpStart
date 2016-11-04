using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WorkshopMVC.Models;

namespace WebAdminAndApi.Models
{
    /// <summary>
    /// Handles the creation of database test data.
    /// </summary>
    public class ImageAppDbInitializer : DropCreateDatabaseIfModelChanges<ImageAppDbContext>
    {
        protected override void Seed(ImageAppDbContext context)
        {
            // TODO: Add some Data for demo purposes
        }
    }
}