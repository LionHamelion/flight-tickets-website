using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class page5 : System.Web.UI.Page
    {
        SQLManager sqlManager = new SQLManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && NavigationManager.ValidateReferrer(Session, "page5.aspx"))
            {
                Session["ExpectedReferrer"] = "page5.aspx";
                BindOrdersGrid();
            }
        }
        private void BindOrdersGrid()
        {
            UserSession userSession = UserSession.RetrieveFromSession();
            // Припустимо, що у нас є метод, який повертає всі замовлення з бази даних
            var ordersList = sqlManager.GetTicketsByName(userSession.UserName);
            OrdersGrid.DataSource = ordersList;
            OrdersGrid.DataBind();
        }
        protected void OrdersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteOrder")
            {
                // Отримати ID замовлення, яке потрібно видалити
                int orderId = Convert.ToInt32(e.CommandArgument);
                // Знайти замовлення у джерелі даних
                Ticket ticket = sqlManager.GetTicketById(orderId);
                if (ticket != null)
                {
                    ticket.StoreInSession();
                    Session["ExpectedReferrer"] = "page6.aspx";
                    Response.Redirect("page6.aspx");
                }
                else
                {
                    throw new Exception("Ticket not found");
                }
            }
        }

    }
}