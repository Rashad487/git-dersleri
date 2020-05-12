using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using writeMeEverything.com.Models;

namespace writeMeEverything.com.ViewModel
{
    public class ViewChat
    {
        public List<Message> Messages { get; set; }

        public User Frend { get; set; }

    }
}