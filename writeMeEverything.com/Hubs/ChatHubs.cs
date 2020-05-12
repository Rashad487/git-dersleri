using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace writeMeEverything.com.Hubs
{
    public class ChatHubs : Hub
    {
        public override Task OnConnected()
        {
            var group = Context.Request.Cookies["token"].Value;
            Groups.Add(Context.ConnectionId, group);
            return base.OnConnected();
        }
    }
}