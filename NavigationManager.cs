using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace Lab5
{
    public class NavigationManager
    {
        public static bool ValidateReferrer(HttpSessionState session, string currentPage)
        {
            string expectedReferrer = session["ExpectedReferrer"] as string;

            // Якщо очікуваний referrer не встановлений або не відповідає поточній сторінці, переадресуйте на сторінку 1
            if (string.IsNullOrEmpty(expectedReferrer) || !expectedReferrer.Equals(currentPage, StringComparison.OrdinalIgnoreCase))
            {
                // Redirect to page1.aspx
                HttpContext.Current.Response.Redirect("page1.aspx");
                return false;
            }

            return true;
        }

    }
}