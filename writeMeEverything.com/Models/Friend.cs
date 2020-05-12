using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverything.com.Models
{
    public class Friend
    {

        public int Id { get; set; }

        [ForeignKey("Sender")]
        [Required]
        public int SenderId { get; set; }

        [ForeignKey("Acceptor")]
        [Required]
        public int AcceptorId { get; set; }

        public string Message { get; set; }

        [Required]
        public bool IsFriends { get; set; }
        [Required]
        public bool IsBlocked { get; set; }

        public User Sender { get; set; }

        public User Acceptor { get; set; }

        public List<Media> Medias { get; set; }


    }
}