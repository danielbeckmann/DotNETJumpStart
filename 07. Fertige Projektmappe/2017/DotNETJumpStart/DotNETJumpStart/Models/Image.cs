using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNETJumpStart.Models
{
    /// <summary>
    /// The image entity
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the images id.
        /// </summary>
        [Key]
        [Index(IsUnique = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the images filename.
        /// </summary>
        [Index(IsUnique = true)]
        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }
    }
}