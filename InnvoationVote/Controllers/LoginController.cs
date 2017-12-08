using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common;
using InnvoationVote.Models;

namespace InnvoationVote.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = null;
                try
                {
                    ticket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (Exception)
                {
                    // ignored
                }
                if (ticket != null)
                {
                    var user = ticket.UserData.JsonToObject<UserInfo>();
                    if (user != null)
                    {
                        return Redirect("~/Home");
                    }
                }
            }
            return View();
        }

        public ActionResult Login(string account, string password, bool remember)
        {
            if (account == "Admin" && password == "e10adc3949ba59abbe56e057f20f883e")
            {
                FormsAuthenticationTicket ticket;
                UserInfo u = new UserInfo();
                u.UserName = account;
                u.UserPwd = password;
                if (remember)
                {
                    
                    ticket = new FormsAuthenticationTicket(1, account, DateTime.Now, DateTime.Now.AddDays(30), false,
                        u.ToJson());
                }
                else
                {
                    ticket = new FormsAuthenticationTicket(1, account, DateTime.Now, DateTime.Now.AddHours(0.5), false, u.ToJson());
                }
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);
                return Json(new { code = 1, data = "登陆成功" });
            }
            return Json(new {code = 0, data = "用户名或者密码错误"});
        }

        public ActionResult LoginOut()
        {
            var authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                authCookie.Expires =DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }
            return Json(new {code = 1, data = "退出成功"});
        }
    }
}