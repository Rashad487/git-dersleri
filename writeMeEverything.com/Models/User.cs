using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverything.com.Models
{
    
    public class User
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Please enter your Firstname!")]
        [MinLength(2, ErrorMessage = "2 letters minimum")]
        [MaxLength(50, ErrorMessage = "50 letters maximum")]
        public string Firstname { get; set; }



        [Required(ErrorMessage = "Please enter your Lastname!")]
        [MinLength(2, ErrorMessage = "2 letters minimum")]
        [MaxLength(50, ErrorMessage = "50 letters maximum")]
        public string Lastname { get; set; }



        [Required(ErrorMessage = "Please enter your Email!")]
        [MinLength(6, ErrorMessage = "6 letters minimum")]
        [MaxLength(50, ErrorMessage = "50 letters maximum")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Please enter your Password!")]
        [MinLength(6, ErrorMessage = "6 letters minimum")]
        [MaxLength(100, ErrorMessage = "100 letters maximum")]
        public string Password { get; set; }



        [Required]
        public bool Verify { get; set; }
        public string VerifyText { get; set; }

        public DateTime Lastseen { get; set; }
        public string Token { get; set; }

        public bool isOnline { get; set; }
        public string ResetText { get; set; }
        public string Avatar { get; set; }

        [MinLength(3, ErrorMessage = "3 letters minimum")]
        [MaxLength(500, ErrorMessage = "500 letters maximum")]
        public string About { get; set; }

        [MinLength(10, ErrorMessage = "10 letters minimum")]
        [MaxLength(10, ErrorMessage = "10 letters maximum")]
        public string Phone { get; set; }
        public string City { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        public List<Friend> Friends { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }
    }
}