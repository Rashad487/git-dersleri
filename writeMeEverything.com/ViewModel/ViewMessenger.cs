using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using writeMeEverything.com.Models;

namespace writeMeEverything.com.ViewModel
{
    public class ViewMessenger
    {
        public User User { get; set; }
        public List<Friend> Friends { get; set; }

        public List<Chat> Chats{ get; set; }

        public List<Message> Messages { get; set; }
    }
}