using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace writeMeEverything.com.ViewModel
{
    public class ViewFirstRegistration
    {
        [Required(ErrorMessage = "Please enter your Firstname!")]
        [MinLength(2, ErrorMessage = "2 letters minimum")]
        [MaxLength(15, ErrorMessage = "15 letters maximum")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter your Lastname!")]
        [MinLength(2, ErrorMessage = "2 letters minimum")]
        [MaxLength(15, ErrorMessage = "15 letters maximum")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter your Email!")]
        [MinLength(6, ErrorMessage = "6 letters minimum")]
        [MaxLength(50, ErrorMessage = "50 letters maximum")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Password!")]
        [MinLength(6, ErrorMessage = "6 letters minimum")]
        [MaxLength(50, ErrorMessage = "50 letters maximum")]
        public string Password { get; set; }
    }
}