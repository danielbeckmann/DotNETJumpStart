using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNETJumpStart.Models.Dtos
{
    /// <summary>
    /// Data transfer object for adding new posts.
    /// </summary>
    public class AddPostDto
    {
        [Required]
        public int ImageId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string UserIdentifier { get; set; }
    }
}