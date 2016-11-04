using System;
using System.Collections.Generic;
using System.Linq;
using ImageApp.Common;
using ImageApp.ViewModels;

namespace ImageApp.DataModel
{
    /// <summary>
    /// Data transfer object for post objects.
    /// </summary>
    public class Post : BindableBase
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string Title { get; set; }

        public string ImageUri { get; set; }

        private int likes;
        public int Likes 
        {
            get { return this.likes; }
            set { this.SetProperty(ref this.likes, value); }
        }

        public DateTime Created { get; set; }
    }
}