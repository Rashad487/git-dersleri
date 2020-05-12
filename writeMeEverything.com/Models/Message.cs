using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverything.com.Models
{
    public class Message
    {
        public int Id { get; set; }

        //[ForeignKey("Sender")]
        //[Required]

        public string Content { get; set; }

        public DateTime CreateAt { get; set; }
        public int SenderId { get; set; }

        //[ForeignKey("Receiver")]
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public int ChatId { get; set; }
        public Chat Chat{ get; set; }

    }


}