using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.WeChatModel;

namespace InnvoationVote.Controllers
{
    public class WeChatController : Controller
    {
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetOpenId(string currentPage)
        {
            var url = string.Format(WeChatConfig.WeChatCodeUrl, WeChatConfig.AppId, HttpUtility.UrlEncode($"http://{Request.Url?.Authority}/WeChat/RedirectUrL?currentPage={currentPage}"));
            ViewBag.CodeUrl = url;
            return View();
        }

        public ActionResult RedirectUrL(string code, string currentPage)
        {
            var data = HttpHelper.HttpGet(string.Format(WeChatConfig.OpenIdUrl, WeChatConfig.AppId, WeChatConfig.AppSecret, code));
            var openId = data.JsonToObject<OpenIdModel>().openid;
            if (openId.Length < 20)
            {
                LogHelper.Log(openId, "可能错误的openId");
            }
            currentPage = HttpUtility.UrlDecode(currentPage);
            ViewBag.RedirectUrL = $"{currentPage}{(currentPage?.IndexOf("?", StringComparison.Ordinal) > -1 ? "&" : "?")}openId={openId}";
            return View();
        }

        public string GetAccessToken()
        {
            if (WeChatConfig.AccessToken == null ||
                WeChatConfig.AccessToken.Value.IsNullOrEmpty() ||
                DateTime.Now.AddHours(-2) > WeChatConfig.AccessToken.Time)
            {
                var data = HttpHelper.HttpGet(string.Format(WeChatConfig.AccessTokenUrl, WeChatConfig.AppId, WeChatConfig.AppSecret));
                var access = data.JsonToObject<AccessTokenModel>();
                LogHelper.Log(data, "记录获取AccessToken");
                if (!access.access_token.IsNullOrEmpty())
                {
                    WeChatConfig.AccessToken = new TokenModel
                    {
                        Value = access.access_token,
                        Time = DateTime.Now
                    };
                }
                return access.access_token;
            }
            return WeChatConfig.AccessToken.Value;
        }

        public string GetJsApiTicket()
        {
            if (WeChatConfig.JsApiTicket == null ||
                DateTime.Now.AddHours(-2) > WeChatConfig.JsApiTicket.Time)
            {
                var url = string.Format(WeChatConfig.JsApiTicketUrl, GetAccessToken());
                var data = HttpHelper.HttpGet(url);
                var access = data.JsonToObject<JsApiTicketModel>();
                LogHelper.Log(data, "记录获取JsApiTicket");
                if (!access.ticket.IsNullOrEmpty())
                {
                    WeChatConfig.JsApiTicket = new TokenModel
                    {
                        Value = access.ticket,
                        Time = DateTime.Now
                    };
                }
                return access.ticket;
            }
            return WeChatConfig.JsApiTicket.Value;
        }

        /// <summary>
        /// 获取JsApi配置信息
        /// </summary>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public ActionResult GetJsApiConfig(string currentPage)
        {
            var nonceStr = ConvertHelper.GetNonce(16);
            var timestamp = ConvertHelper.GetTimeStamp();
            var str = "jsapi_ticket=" + GetJsApiTicket()
                      + "&noncestr=" + nonceStr
                      + "&timestamp=" + timestamp
                      + "&url=" + currentPage;
            return Json(new
            {
                appId = WeChatConfig.AppId,
                timestamp,
                nonceStr,
                signature = str.ToSha1(),
                jsApiList = new ArrayList
                {
                    "onMenuShareTimeline",
                    "onMenuShareAppMessage",
                    "onMenuShareQQ",
                    "onMenuShareWeibo",
                    "onMenuShareQZone",
                    "startRecord",
                    "stopRecord",
                    "onVoiceRecordEnd",
                    "playVoice",
                    "pauseVoice",
                    "stopVoice",
                    "onVoicePlayEnd",
                    "uploadVoice",
                    "downloadVoice",
                    "chooseImage",
                    "previewImage",
                    "uploadImage",
                    "downloadImage",
                    "translateVoice",
                    "getNetworkType",
                    "openLocation",
                    "getLocation",
                    "hideOptionMenu",
                    "showOptionMenu",
                    "hideMenuItems",
                    "showMenuItems",
                    "hideAllNonBaseMenuItem",
                    "showAllNonBaseMenuItem",
                    "closeWindow",
                    "scanQRCode",
                    "chooseWXPay",
                    "openProductSpecificView",
                    "addCard",
                    "chooseCard",
                    "openCard"
                }
            });

        }
    }
}