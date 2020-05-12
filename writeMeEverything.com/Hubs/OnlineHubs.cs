using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using writeMeEverything.com.Data;
using writeMeEverything.com.Models;

namespace writeMeEverything.com.Hubs
{
    public class OnlineHubs : Hub
    {
        protected readonly ChatContext _context = new ChatContext();

        public override Task OnConnected()
        {
            var token = Context.Request.Cookies["token"].Value;
            Groups.Add(Context.ConnectionId, token);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {


            var token = Context.Request.Cookies["token"].Value;
            Groups.Remove(Context.ConnectionId, token);
            return base.OnDisconnected(stopCalled);

        }



    }
}