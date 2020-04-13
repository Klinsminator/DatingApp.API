using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { set; get; }

        public string Username { set; get; }

        public byte[] PasswordHash { set; get; }

        public byte[] PasswordSalt { set; get; }

        public string Gender { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string KnownAs { set; get; }

        public DateTime Created { set; get; }

        public DateTime LastActive { set; get; }

        public string Introduction { set; get; }

        public string LookingFor { set; get; }

        public string Interests { set; get; }

        public string City { set; get; }

        public string Country { set; get; }

        public ICollection<Photo> Photos { set; get; }

        public ICollection<Like> Likers { get; set; }

        public ICollection<Like> Likees { get; set; }

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<Message> MessagesRecieved { get; set; }
    }
}
