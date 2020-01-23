using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { set; get; }

        public string Username { set; get; }

        public string Gender { set; get; }

        public int Age { set; get; }

        public string KnownAs { set; get; }

        public DateTime Created { set; get; }

        public DateTime LastActive { set; get; }

        public string City { set; get; }

        public string Country { set; get; }

        public string PhotoUrl { set; get; }
    }
}
