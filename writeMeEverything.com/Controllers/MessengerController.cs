using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using writeMeEverything.com.Filter;
using writeMeEverything.com.Hubs;
using writeMeEverything.com.Models;
using writeMeEverything.com.ViewModel;

namespace writeMeEverything.com.Controllers
{
    [Auth]
    public class MessengerController : BaseController
    {
        // GET: Messenger
        public ActionResult Index()
        {
            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);
            if (user != null)
            {
                ViewMessenger messenger = new ViewMessenger
                {
                    User = user,
                    Friends = _context.Friends.Include("Sender").Include("Acceptor").Where(f => (f.SenderId == user.Id || f.AcceptorId == user.Id) && f.IsFriends == true && f.IsBlocked == false).ToList(),
                    Chats = _context.Chats.Include("Messages").Where(c => c.SenderId == user.Id || c.ReceiverId == user.Id).ToList(),
                    Messages = _context.Messages.Include("Chat").Where(c => c.SenderId == user.Id || c.ReceiverId == user.Id).ToList()

                };
                return View(messenger);
            }
            return View();
        }

        public ActionResult Chat(int Id)
        {

            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);

            if (user != null)
            {

                ViewChat chat = new ViewChat
                {
                    Messages = _context.Messages.Where(m => m.SenderId == Id && m.ReceiverId == user.Id || m.ReceiverId == Id && m.SenderId == user.Id).OrderBy(m => m.CreateAt).ToList(),
                    Frend = _context.Users.FirstOrDefault(u => u.Id == Id)
                };



                return PartialView("_chat", chat);
            }
            return View();
        }

        public ActionResult AboutUser(int Id)
        {

            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);

            if (user != null)
            {
                User friend = _context.Users.FirstOrDefault(u => u.Id == Id);
                return PartialView("_chatAbout", friend);
            }
            return View();
        }

        public void AddFriend(string Email, string Message)
        {

            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);
            User acceptor = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user != null && acceptor != null)
            {
                Friend friend = new Friend
                {
                    SenderId = user.Id,
                    AcceptorId = acceptor.Id,
                    Message = Message,
                    IsFriends = false,
                    IsBlocked = false

                };

                _context.Friends.Add(friend);
                _context.SaveChanges();
                Response.Cookies["message"].Value = "Request has been sent: " + Email;


                var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotiHubs>();
                hubContext.Clients.Group(acceptor.Token).showNoti(user.Firstname + " " + user.Lastname + " sent you a friend request ", "success");
            }

        }

        public void AddMessage(int Id, string Message)
        {

            string Token = Request.Cookies["token"].Value;
            User sender = _context.Users.FirstOrDefault(u => u.Token == Token);
            User receiver = _context.Users.FirstOrDefault(u => u.Id == Id);
            if (sender != null && receiver != null)
            {

                Chat ct = _context.Chats.FirstOrDefault(c => c.ReceiverId == receiver.Id && c.SenderId == sender.Id || c.ReceiverId == sender.Id && c.SenderId == receiver.Id);
                if (ct == null)
                {
                    Chat newct = new Chat
                    {
                        Receiver = receiver,
                        Sender = sender,
                        CreateAt = DateTime.Now.AddHours(4)

                    };
                    _context.Chats.Add(newct);
                    _context.SaveChanges();
                    ct = _context.Chats.FirstOrDefault(c => c.ReceiverId == receiver.Id && c.SenderId == sender.Id || c.ReceiverId == sender.Id && c.SenderId == receiver.Id);
                }

                Message msg = new Message
                {
                    SenderId = sender.Id,
                    ReceiverId = Id,
                    Content = Message,
                    CreateAt = DateTime.Now,
                    ChatId = ct.Id
                };

                _context.Messages.Add(msg);
                _context.SaveChanges();

                if (receiver.Token != null || receiver.Token != "")
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHubs>();
                    hubContext.Clients.Group(receiver.Token).showMessage(msg.Content, sender.Id);
                }
            }

        }

        public void AcceptOrDeleteFriend(int Id, string Opr)
        {

            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);

            if (user != null)
            {
                Friend friend = _context.Friends.Include("Sender").FirstOrDefault(f => f.SenderId == Id && f.AcceptorId == user.Id && f.IsFriends == false && f.IsBlocked == false);
                if (friend != null)
                {
                    if (Opr == "accept")
                    {
                        friend.IsFriends = true;
                        Response.Cookies["message"].Value = "You added as friend: " + friend.Sender.Email;
                    }
                    else
                    {
                        Response.Cookies["message"].Value = "You delete as friend requests: " + friend.Sender.Lastname;
                        _context.Friends.Remove(friend);

                    }
                    _context.SaveChanges();

                }

            }
        }


        public ActionResult NotFriend()
        {

            string Token = Request.Cookies["token"].Value;
            User user = _context.Users.FirstOrDefault(u => u.Token == Token);
            if (user != null)
            {

                List<Friend> friend = _context.Friends.Include("Sender").Include("Acceptor").Where(f => f.AcceptorId == user.Id && f.IsFriends == false && f.IsBlocked == false).ToList();
                return PartialView("_notFriend", friend);

            }
            return View();

        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("index", "home");

        }


        //[HttpPost]
        //public ActionResult Index(User user)
        //{
        //    if (user.File != null)
        //    {
        //        if (user.File.ContentType != "image/png" && user.File.ContentType != "image/jpeg")
        //        {
        //            ModelState.AddModelError("File", "You can download the png or jpg file");
        //        }

        //        if (user.File.ContentLength / 1024 / 1024 > 1)
        //        {
        //            ModelState.AddModelError("File", "You can download max 1 MB");
        //        }
        //    }



        //    if (ModelState.IsValid)
        //    {
        //        if (user.File != null)
        //        {
        //            System.IO.File.Delete(Path.Combine(Server.MapPath("/wwwroot/userData/profile_image"), user.Avatar));

        //            var texts = user.File.FileName.Split('.');
        //            user.Avatar = Guid.NewGuid().ToString() + "." + texts[texts.Length - 1];

        //            string path = Path.Combine(Server.MapPath("/wwwroot/userData/profile_image"), user.Avatar);

        //            user.File.SaveAs(path);
        //        }



        //        _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        //        _context.SaveChanges();
        //        Response.Cookies["message"].Value = "Changes is Saved";

        //    }
        //    return View();
        //}
    }
}   
