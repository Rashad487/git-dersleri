using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using writeMeEverything.com.Models;
using writeMeEverything.com.ViewModel;
using System.Net.Mail;
using System.Net;

namespace writeMeEverything.com.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login log)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(u => u.Email == log.Email);
                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.Password, log.Password))
                    {
                        if (user.Verify)
                        {
                           
                            Session["UserId"] = user.Id;

                            user.Token = Guid.NewGuid().ToString();
                            user.isOnline = true;

                            var tokenCookie = new HttpCookie("token")
                            {
                                Value = user.Token,
                                Expires = DateTime.Now.AddYears(1)
                            };

                            Response.Cookies.Add(tokenCookie);

                            _context.SaveChanges();


                            return RedirectToAction("index", "messenger");
                        }
                        else
                        {
                            Response.Cookies["message"].Value = "To complete your sign up, please verify your email: " + user.Email;
                        }
                    }
                    else 
                    {
                        Response.Cookies["message"].Value = "Your Email or Password is wrong";

                    }
                }
                else 
                {
                    Response.Cookies["message"].Value = "Your Email or Password is wrong";

                }
            }
            return View(log);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ViewFirstRegistration firstRUser)
        {
            if (ModelState.IsValid)
            {
                User userR = _context.Users.FirstOrDefault(u => u.Email == firstRUser.Email);
                if (userR!=null)
                {
                    Response.Cookies["message"].Value = "This email adress already exists, please login or reset password ";
                    return View(firstRUser);
                }
                //create user
                User user = new User
                {
                    Firstname = firstRUser.Firstname,
                    Lastname = firstRUser.Lastname,
                    Email = firstRUser.Email,
                    Password = Crypto.HashPassword(firstRUser.Password),
                    Verify = false,
                    Avatar="0.gif",
                    isOnline=false,
                    VerifyText = Crypto.GenerateSalt(),
                    Lastseen = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // create mail
                var body = @"<div class=""""row""""><div class=""col-md-12""><table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #eaf0f7; margin: 0;"" bgcolor=""#eaf0f7"">
                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                        <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                                        <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                                            valign=""top"">
                                            <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 50px; 0"">
                                                <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px dashed #4d79f6;""
                                                       bgcolor=""#fff"">
                                                    <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                        <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                                            <meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                            <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <tr>
                                                                    <td><a href=""#""><img src=""./dist/media/svg/soho-logo.svg"" alt="""" style=""margin-left: auto; margin-right: auto; display:block; margin-bottom: 10px; height: 80px;""></a></td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #2dc57f; font-size: 24px; font-weight: 700; text-align: center; vertical-align: top; margin: 0; padding: 0 0 10px;""
                                                                        valign=""top"">{0}! Welcome to WriteMeEverything.com</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #3f5db3; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">Please confirm your email address by clicking the link below.</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">We may need to send you critical information about our service and it is important that we have an accurate email address.</td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;""
                                                                        valign=""top""><a href=""{1}"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: block; border-radius: 5px; text-transform: capitalize; background-color: #2dc57f; margin: 0; border-color: #2dc57f; border-style: solid; border-width: 10px 20px;"">Confirm email address</a></td>
                                                                </tr>
                                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                    <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; padding-top: 5px; vertical-align: top; margin: 0; text-align: right;"" valign=""top"">&mdash; <b>WriteMeEverything.com</b> - Discussion and Chat Application</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.Email));
                message.From = new MailAddress("yaxcioglan@mail.com");  // replace with valid value
                message.Subject = "Verify Account";
                message.Body = string.Format(body, user.Firstname, "http://writemeeverything.com/Home/VerifyAccount?verifyText=" + user.VerifyText);
              //  message.Body = string.Format(body, user.Firstname, "https://localhost:44329/Home/VerifyAccount?verifyText=" + user.VerifyText);
                message.IsBodyHtml = true;

                //send mail with smtp
                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "agasef.memmedli221@gmail.com",  // replace with valid value
                            Password = "seynur2462736"  // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                        Response.Cookies["message"].Value = "To complete your sign up, please verify your email: " + user.Email;
                        return  RedirectToAction("index", "home");
                    }
            }
            return View(firstRUser);
        }

        // if click link in email you redirect in this action
        public ActionResult VerifyAccount(string verifyText)
        {
            User user = _context.Users.FirstOrDefault(u=>u.VerifyText==verifyText);
            if (user != null)
            {
                user.Verify = true;
                user.VerifyText = "Verified";
                _context.SaveChanges();
                Response.Cookies["message"].Value = user.Firstname + ' ' + user.Lastname + " your account is Verifyed";
            }
            
            return RedirectToAction("index", "home");

        }

        public ActionResult ResetPassword()
        {
            return View();
        }


        //change password
        [HttpPost]
        public ActionResult ResetPassword(Email email)
        {

            User userR = _context.Users.FirstOrDefault(u => u.Email == email.uEmail);
            if (userR == null)
            {
                Response.Cookies["message"].Value = "This email adress not exists";
                return View(email);
            }

            userR.ResetText = Crypto.GenerateSalt();

            _context.SaveChanges();

            // create mail
            var body = @"<div class=""""row""""><div class=""col-md-12""><table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #eaf0f7; margin: 0;"" bgcolor=""#eaf0f7"">
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                    <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                                    <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                                        valign=""top"">
                                        <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 50px; 0"">
                                            <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px dashed #4d79f6;""
                                                    bgcolor=""#fff"">
                                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                    <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                                        <meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                            <tr>
                                                                <td><a href=""#""><img src=""./dist/media/svg/soho-logo.svg"" alt="""" style=""margin-left: auto; margin-right: auto; display:block; margin-bottom: 10px; height: 80px;""></a></td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #2dc57f; font-size: 24px; font-weight: 700; text-align: center; vertical-align: top; margin: 0; padding: 0 0 10px;""
                                                                    valign=""top"">{0}! Welcome to WriteMeEverything.com</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; color: #3f5db3; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">Please to reset your password, follow the link.</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;"" valign=""top"">We may need to send you critical information about our service and it is important that we have an accurate email address.</td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 10px 10px;""
                                                                    valign=""top""><a href=""{1}"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: block; border-radius: 5px; text-transform: capitalize; background-color: #2dc57f; margin: 0; border-color: #2dc57f; border-style: solid; border-width: 10px 20px;"">Reset Password</a></td>
                                                            </tr>
                                                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                                                <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; padding-top: 5px; vertical-align: top; margin: 0; text-align: right;"" valign=""top"">&mdash; <b>WriteMeEverything.com</b> - Discussion and Chat Application</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(userR.Email));
            message.From = new MailAddress("yaxcioglan@mail.com");  // replace with valid value
            message.Subject = "Reset Password";
            message.Body = string.Format(body, userR.Firstname, "http://writemeeverything.com/Home/Reset?resetText=" + userR.ResetText);

          //  message.Body = string.Format(body, userR.Firstname, "https://localhost:44329/Home/Reset?resetText=" + userR.ResetText);
            message.IsBodyHtml = true;

            //send mail with smtp
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "agasef.memmedli221@gmail.com",  // replace with valid value
                    Password = "seynur2462736"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
                Response.Cookies["message"].Value = "For reset your password, please check your email: " + userR.Email;
                return RedirectToAction("index", "home");
            }
            

        }

        // reset password with mail
        public ActionResult Reset(string resetText)
        {
            User user = _context.Users.FirstOrDefault(u => u.ResetText == resetText);
            if (user != null)
            {
                _context.SaveChanges();
                ResetPassword rp = new ResetPassword
                {
                    ResetText = user.ResetText,
                    NewPassword = ""
                };
                user.ResetText = "";

                return View(rp);
            }
            Response.Cookies["message"].Value = "User not found1";
            return RedirectToAction("index");

        }
        [HttpPost]
        public ActionResult Reset(ResetPassword rp)
        { 

            User user = _context.Users.FirstOrDefault(u => u.ResetText == rp.ResetText);
            if (user != null)
            {
                user.ResetText = "";
                user.Password = Crypto.HashPassword(rp.NewPassword);
                _context.SaveChanges();

                Response.Cookies["message"].Value = "Password in your account is Reset";
                return RedirectToAction("index", "home");
            }
            Response.Cookies["message"].Value = "User not found";
            return RedirectToAction("index", "home");

        }
    }
}