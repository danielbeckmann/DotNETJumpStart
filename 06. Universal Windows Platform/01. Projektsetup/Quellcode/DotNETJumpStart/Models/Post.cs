using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNETJumpStart.Models;

namespace DotNETJumpStart.Models
{
    /// <summary>
    /// The post entity which defines a posted image in the app.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="Post"/> entity.
        /// </summary>
        public Post()
        {
            this.Created = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the posts creation date and time.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets the posts creation date as string.
        /// </summary>
        [NotMapped]
        public string CreatedShort
        {
            get
            {
                return this.Created.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets or sets the posts id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the related image to the post.
        /// </summary>
        public virtual Image Image { get; set; }

        /// <summary>
        /// Gets or sets the related likes to the post.
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }

        /// <summary>
        /// Gets or sets the posts title.
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user who has created the post.
        /// </summary>
        [Display(Name = "Ersteller")]
        public virtual User User { get; set; }
    }
}