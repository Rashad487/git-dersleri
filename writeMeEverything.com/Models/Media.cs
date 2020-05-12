using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace writeMeEverything.com.Models
{
    public class Media
    {
        public int Id { get; set; }

        public int FriendId { get; set; }

        public string FileName { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }


        public Friend Friends { get; set; }
    }
}