using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { set; get; }

        public string Url { set; get; }

        public string Description { get; set; }

        public DateTime DateAdded { set; get; }

        public bool IsMain { set; get; }

        public User User { set; get; }

        public int UserId { set; get; }
    }
}