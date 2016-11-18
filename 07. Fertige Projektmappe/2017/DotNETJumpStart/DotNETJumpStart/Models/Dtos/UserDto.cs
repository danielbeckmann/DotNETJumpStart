using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNETJumpStart.Models.Dtos
{
    /// <summary>
    /// Data transfer object for user objects.
    /// </summary>
    public class UserDto
    {
        [Required]
        [MaxLength(100)]
        public string Identifier { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public static UserDto Map(User user)
        {
            return new UserDto { Identifier = user.Identifier, Name = user.Name };
        }
    }
}