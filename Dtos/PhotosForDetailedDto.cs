using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class PhotosForDetailedDto
    {
        public int Id { set; get; }

        public string Url { set; get; }

        public string Description { get; set; }

        public DateTime DateAdded { set; get; }

        public bool IsMain { set; get; }
    }
}
