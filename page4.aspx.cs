using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class page4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && NavigationManager.ValidateReferrer(Session, "page4.aspx"))
            {
                Session["ExpectedReferrer"] = "page4.aspx";
                Ticket ticket = Ticket.RetrieveFromSession();
                ReportComposer reportComposer = new ReportComposer();
                string reportHTML = reportComposer.GenerateReportHtml(ticket);
                ReportContainer.Text = reportHTML;
            }
        }

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page1.aspx";
            Response.Redirect("page1.aspx");
        }
    }
}