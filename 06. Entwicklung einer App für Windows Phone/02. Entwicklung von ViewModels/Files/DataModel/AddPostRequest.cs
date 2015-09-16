using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageApp.DataModel
{
    public class AddPostRequest
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string UserIdentifier { get; set; }
    }
}
