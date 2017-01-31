using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNETJumpStart.Models;

namespace DotNETJumpStart.Models
{
    /// <summary>
    /// Handles the creation of database test data.
    /// </summary>
    public class ImageAppDbInitializer : DropCreateDatabaseIfModelChanges<ImageAppDbContext>
    {
        protected override void Seed(ImageAppDbContext context)
        {
            // Create some demo users
            var users = new List<User>
            {
                new User { Identifier = "6fd7d591-0470-41b6-a7a7-0e040bd16638", Name = "Admin" },
                new User { Identifier = "6fd7d591-0470-41b6-a7a7-0e040bd16639", Name = "Dilbert" }
            };

                users.ForEach(s => context.Users.Add(s));
                context.SaveChanges();

                // Create some images
                var images = new List<Image>
            {
                new Image { FileName = "business-q-c-1024-768-9.jpg" },
                new Image { FileName = "cats-q-c-1024-768-4.jpg" },
                new Image { FileName = "city-q-c-1024-768-9.jpg" },
                new Image { FileName = "sports-q-c-1024-768-4.jpg" },
                new Image { FileName = "technics-q-c-1024-768-4.jpg" }
            };

                images.ForEach(s => context.Images.Add(s));
                context.SaveChanges();

                // Create some posts
                var posts = new List<Post>
            {
                new Post { Title = "Business stuff", User = users.First(), Image = images.First() },
                new Post { Title = "My cat", User = users.First(), Image = images.Skip(1).First() },
                new Post { Title = "Random bridge", User = users.Skip(1).First(), Image = images.Skip(2).First() },
                new Post { Title = "Surfin' U.S.A.", User = users.Skip(1).First(), Image = images.Skip(3).First() },
                new Post { Title = "Technics", User = users.First(), Image = images.Skip(4).First() }
            };

                posts.ForEach(s => context.Posts.Add(s));
                context.SaveChanges();

                // Create some likes
                var likes = new List<Like>
            {
                new Like { Post = posts.Skip(4).First(), User = users.First() },
                new Like { Post = posts.Skip(4).First(), User = users.Skip(1).First() },
                new Like { Post = posts.Skip(2).First(), User = users.First() },
            };

            likes.ForEach(s => context.Likes.Add(s));
            context.SaveChanges();
        }
    }
}