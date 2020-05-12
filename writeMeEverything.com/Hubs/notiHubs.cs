using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.SignalR;
using writeMeEverything.com.Data;
using writeMeEverything.com.Models;

namespace writeMeEverything.com.Hubs
{
    public class NotiHubs : Hub
    {


       
        protected readonly ChatContext _context = new ChatContext();

       

        public override Task OnConnected()
        {
            var token = Context.Request.Cookies["token"].Value;
            Groups.Add(Context.ConnectionId, token);
            User ur = _context.Users.FirstOrDefault(u => u.Token == token);
            ur.isOnline =true;


            List<Friend> friends = _context.Friends.Include("Sender").Include("Acceptor").Where(f => (f.SenderId == ur.Id || f.AcceptorId == ur.Id) && f.IsFriends == true && f.IsBlocked == false && f.Acceptor.isOnline == true && f.Sender.isOnline == true).ToList();
            foreach (var item in friends)
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotiHubs>();

                if (item.AcceptorId != ur.Id)
                {
                    hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, "online");

                }
                else 
                {
                    hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, "online");

                }

            }

            _context.SaveChanges();
            return base.OnConnected();
        }


       ////public override Task OnReconnected()
       // {
       //     var token = Context.Request.Cookies["token"].Value;
       //     Groups.Add(Context.ConnectionId, token);
       //     User ur = _context.Users.FirstOrDefault(u => u.Token == token);
       //     ur.isOnline = true;
       //     _context.SaveChanges();
       //     var hubContext = GlobalHost.ConnectionManager.GetHubContext<OnlineHubs>();
       //     List<Friend> friends = _context.Friends.Include("Sender").Include("Acceptor").Where(f => (f.SenderId == ur.Id || f.AcceptorId == ur.Id) && f.IsFriends == true && f.IsBlocked == false && f.Acceptor.isOnline == true && f.Sender.isOnline == true).ToList();
       //     foreach (var item in friends)
       //     {
       //         if (item.AcceptorId == ur.Id)
       //         {
       //             hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, "online");

       //         }
       //         else
       //         {
       //             hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, "online");

       //         }

       //     }
       //     return base.OnReconnected();
       // }
        public override Task OnDisconnected(bool stopCalled)
        {
            var token = Context.Request.Cookies["token"].Value;
            User ur = _context.Users.FirstOrDefault(u => u.Token == token);
            ur.isOnline = false;
            ur.Lastseen = DateTime.Now;
            _context.SaveChanges();
            List<Friend> friends = _context.Friends.Include("Sender").Include("Acceptor").Where(f => (f.SenderId == ur.Id || f.AcceptorId == ur.Id) && f.IsFriends == true && f.IsBlocked == false && f.Acceptor.isOnline == true && f.Sender.isOnline == true).ToList();
            foreach (var item in friends)
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<OnlineHubs>();

                if (item.AcceptorId == ur.Id)
                {
                    hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, ur.Lastseen);

                }
                else
                {
                    hubContext.Clients.Group(item.Sender.Token).showOnline(ur.Id, ur.Lastseen);

                }

            }


            Groups.Remove(Context.ConnectionId, token);

            return base.OnDisconnected(stopCalled);

        }

    }
}