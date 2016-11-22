using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.DataModel;

namespace ImageApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private List<Post> posts;

        /// <summary>
        /// Gets or sets the list of posts.
        /// </summary>
        public List<Post> Posts
        {
            get { return this.posts; }
            set { this.SetProperty(ref this.posts, value); }
        }

        public MainViewModel()
        {
            this.posts = new List<Post>
            {
                new Post { Title = "Baseball", ImageUri = "http://lorempixel.com/400/300/sports/1" },
                new Post { Title = "Surfing", ImageUri = "http://lorempixel.com/400/300/sports/2" },
                new Post { Title = "Cat", ImageUri = "http://lorempixel.com/400/300/cats/1" },
                new Post { Title = "Another cat", ImageUri = "http://lorempixel.com/400/300/cats/5" }
            };
        }
    }
}
