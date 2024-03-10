using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Lab5
{
    public partial class page6 : System.Web.UI.Page
    {
        readonly SQLManager sqlManager = new SQLManager();
        readonly ReportComposer reportComposer = new ReportComposer();
        Ticket ticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && NavigationManager.ValidateReferrer(Session, "page6.aspx"))
            {
                Session["ExpectedReferrer"] = "page6.aspx";
                ticket = Ticket.RetrieveFromSession();
                string reportHTML = reportComposer.GenerateReportHtml(ticket);
                ReportContainer.Text = reportHTML;
            }
        }
        protected void btnDeleteAndEmail_Click(object sender, EventArgs e)
        {
            UserSession userSession = UserSession.RetrieveFromSession();
            string email = userSession.UserEmail;
            ticket = Ticket.RetrieveFromSession();

            //Видалення фото з локальних файлів
            //string photosPath = Path.Combine(Server.MapPath("~/"), "photos\\");
            //string filePath = photosPath + ticket.PhotoPath;
            //File.Delete(filePath);


            string reportHTML = "Дуже шкода, що ви скасували замовлення. Кошти на ваш рахунок будуть невдовзі повернуті";
            reportHTML += reportComposer.GenerateReportHtml(ticket);
             
            sqlManager.DeleteTicketById(ticket.Id);
            SMTPManager.SendEmail(email, "Видалення замовлення авіаквитка", reportHTML);
            Session["ExpectedReferrer"] = "page7.aspx";
            Response.Redirect("page7.aspx");
        }
    }
}