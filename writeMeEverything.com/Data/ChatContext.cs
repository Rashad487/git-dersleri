using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using writeMeEverything.com.Models;

namespace writeMeEverything.com.Data
{
    public class ChatContext:DbContext
    {
        public ChatContext() : base("ChatContext")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Media> Medias { get; set; }

        public DbSet<Message> Messages { get; set; }
        
    }
}