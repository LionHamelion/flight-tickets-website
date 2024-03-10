using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public void StoreInSession()
        {
            HttpContext.Current.Session["UserName"] = UserName;
            HttpContext.Current.Session["UserEmail"] = UserEmail;
        }

        public static UserSession RetrieveFromSession()
        {
            var session = new UserSession
            {
                UserName = HttpContext.Current.Session["UserName"] as string,
                UserEmail = HttpContext.Current.Session["UserEmail"] as string
            };
            return session;
        }
    }

}