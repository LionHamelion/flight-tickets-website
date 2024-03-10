using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class page1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ExpectedReferrer"] = "page1.aspx";
            }
        }

        protected void ExistingOffersBtn_Click(object sender, EventArgs e)
        {
            InitializeSession();
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page5.aspx";
            Response.Redirect("page5.aspx");
        }

        protected void NewOfferBtn_Click(object sender, EventArgs e)
        {
            InitializeSession();
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page2.aspx";
            Response.Redirect("page2.aspx");
        }
        protected void InitializeSession()
        {
            string username = nameInput.Text;
            string email = emailInput.Text;
            UserSession userSession = new UserSession
            {
                UserName = username,
                UserEmail = email
            };
            userSession.StoreInSession();
        }
    }
}