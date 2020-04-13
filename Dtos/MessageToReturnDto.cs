using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        // Automapper should be able to recognize that the SenderKnownAs would match the
        // actual Sender (User class) properties such as KnownAs, PhotoUrl... 
        // that is why should have the properties after the Sender and Recipient equal as on the user class
        public string SenderKnownAs { get; set; }

        public string SenderPhotoUrl { get; set; }

        public int RecipientId { get; set; }

        public string RecipientKnownAs { get; set; }

        public string RecipientPhotoUrl { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public DateTime MessageSent { get; set; }
    }
}
