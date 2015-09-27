﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAdminAndApi.Models;

namespace WorkshopMVC.Models
{
    /// <summary>
    /// The user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the users id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier which maps to a device-id.
        /// </summary>
        [Index(IsUnique = true)]
        [Required]
        [MaxLength(100)]
        public string Identifier { get; set; }

        // TODO: Add the Property "Name"

        /// <summary>
        /// Gets or sets the users likes for the posts.
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }

        // TODO: Add the Property "Posts"
    }
}