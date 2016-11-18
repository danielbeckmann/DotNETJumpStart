using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNETJumpStart.Models.Dtos
{
    /// <summary>
    /// Data transfer object for like objects.
    /// </summary>
    public class LikeDto
    {
        public string UserIdentifier { get; set; }

        public int PostId { get; set; }
    }
}