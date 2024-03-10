using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Lab5
{
    public partial class page3 : System.Web.UI.Page
    {
        SQLManager sqlManager = new SQLManager();
        Ticket ticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && NavigationManager.ValidateReferrer(Session, "page3.aspx"))
            {
                Session["ExpectedReferrer"] = "page3.aspx";
                ticket = Ticket.RetrieveFromSession();
                InitializeClientCalculator();
                string imgUrl = ResolveUrl("~/photos/" + ticket.PhotoPath);
                ProfileImage.ImageUrl = imgUrl;
                NameLabel.Text = ticket.Name;
            }
        }
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page2.aspx";
            Response.Redirect("page2.aspx");
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            ticket = Ticket.RetrieveFromSession();
            ticket.IsBusinessClass = ClassSelection.SelectedItem.Value == "business";
            ticket.IsAdult = AgeSelection.SelectedItem.Value == "adult";
            ticket.Luggage = LuggageCheckbox.Checked;
            ticket.AccompanyingAdult = Convert.ToInt32(AdultCount.Text);
            ticket.AccompanyingChild = Convert.ToInt32(ChildCount.Text);
            ticket.Price = ticket.CalculatePrice();
                        
            sqlManager.AddTicket(ticket);

            ReportComposer reportComposer = new ReportComposer();

            string reportHTML = "Спасибі, що замовили у нас квиток. Кошти з вашого рахунку буде знято автоматично";
            reportHTML += reportComposer.GenerateReportHtml(ticket);


            UserSession userSession = UserSession.RetrieveFromSession();
            SMTPManager.SendEmail(userSession.UserEmail, "Нове замовлення авіаквитка", reportHTML);
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page4.aspx";
            Response.Redirect("page4.aspx");
        }
        private void InitializeClientCalculator()
        {
            string script = $@"
            <script>
                var departureCity = '{ticket.DepartureCity}';
                var arrivalCity = '{ticket.ArrivalCity}';
                var isReturnTicket = {ticket.IsReturnTicket.ToString().ToLower()};
            </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "initializeValues", script);
        }
    }
}