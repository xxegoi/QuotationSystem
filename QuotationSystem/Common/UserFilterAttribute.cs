using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuotationSystem.Common
{
    public class UserFilterAttribute:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.Result = new RedirectResult("/Employee/Login");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //验证是否有登录，无登录则重定向到登录页面
            if (httpContext.Session["User"] == null || httpContext.Session["User"].ToString() == "")
            {
                return false;
            }
            else
            {
                //获取COOKIE信息,并与数据库中值进行比较是否存在，不存在则重定向到登录页面
                var cookie = httpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

                Models.QSDbContext db = new Models.QSDbContext();
                if (db.UserLoginHistory.Count(p => p.Cookie == cookie.Value) > 0)
                {
                    //反序列化COOKIE信息
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //检查COOKIE是否过期，过期则返回到返回登录页面
                    if (ticket.Expired)
                    {
                        return false;
                    }

                    //验证通过
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}