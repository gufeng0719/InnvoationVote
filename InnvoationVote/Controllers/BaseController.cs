using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using Common;
using InnvoationVote.Models;

namespace InnvoationVote.Controllers
{
    public class BaseController : Controller
    {
        protected UserInfo user;
        protected string OrderNumberFormat = "yyyyMMddHHmmssffff";

        protected internal JsonResult Json(ResModel data)
        {
            var res = new JsonResult
            {
                Data = new
                {
                    code = data.ResStatus.GetHashCode(),
                    data = data.Data
                }
            };
            return res;
        }

        protected const int PageSize = 50;

        protected override void OnAuthentication(AuthenticationContext filterContext)
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
                    LogOut(filterContext);
                }
                if (ticket != null)
                {
                    user = ticket.UserData.JsonToObject<UserInfo>();
                    ViewBag.UserName = user.UserName;
                    if (user == null)
                    {
                        LogOut(filterContext);
                    }
                }
                else
                {
                    LogOut(filterContext);
                }
            }
            else
            {
                LogOut(filterContext);
            }
        }

        private void LogOut(AuthenticationContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<script>window.location.href = '/Login'</script>");
            HttpContext.Response.End();
        }
    }

    public class ResModel
    {
        public ResStatue ResStatus { get; set; } = ResStatue.Yes;
        public object Data { get; set; }
    }

}

public enum ResStatue
{
    Yes =1,
    No=0,
    Warn =2,
    LoginOut=3
}