using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAdminAndApi.Models;

namespace WorkshopMVC.Models
{
    /// <summary>
    /// The like entity.
    /// </summary>
    public class Like
    {
        public Like()
        {
            this.Created = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the likes id.
        /// </summary>
        [Key]
        [Index(IsUnique = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the related user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the related post.
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Gets or sets the posts creation date and time.
        /// </summary>
        public DateTime Created { get; set; }
    }
}