using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Lab5
{
    public class ReportComposer
    {
        public string GenerateReportHtml(Ticket ticket)
        {

            string name = ticket.Name;
            string photoUrl = GetPhotoUrl(ticket.PhotoPath);
            string departureCity = ticket.DepartureCity;
            string arrivalCity = ticket.ArrivalCity;
            DateTime flightDate = ticket.FlightDate;
            DateTime? returnDate = ticket.ReturnDate;
            bool isReturnTicket = ticket.IsReturnTicket;
            bool isBusinessClass = ticket.IsBusinessClass;
            bool isAdult = ticket.IsAdult;
            bool luggage = ticket.Luggage;
            int accompanyingAdult = ticket.AccompanyingAdult;
            int accompanyingChild = ticket.AccompanyingChild;
            int price = ticket.Price;

            string returnTicketDisplay = isReturnTicket ? "Так" : "Ні";
            string ticketTypeDisplay = isBusinessClass ? "Бізнес" : "Економ";
            string adultDisplay = isAdult ? "Від 7 років" : "Від 2 до 7 років";
            string luggageDisplay = luggage ? "Так" : "Ні";

            string formattedPrice = $"{price:0.00}₴";

            var replacements = new Dictionary<string, object>
            {
                {"$name$", name },
                {"$photoUrl$", photoUrl },
                {"$departureCity$", departureCity},
                {"$arrivalCity$", arrivalCity},
                {"$flightDate$", flightDate },
                {"$returnDate$", returnDate},
                {"$isReturnTicket$", returnTicketDisplay},
                {"$classType$", ticketTypeDisplay },
                {"$isAdult$", adultDisplay },
                {"$luggage$", luggageDisplay },
                {"$accompanyingAdult$", accompanyingAdult },
                {"$accompanyingChild$", accompanyingChild },
                {"$price$", formattedPrice },
            };

            string pageHTML = TemplateParser.ParseTemplate(HttpContext.Current.Server.MapPath("~/EmailTemplate.tt"), replacements);

            return pageHTML;
        }

        private string GetPhotoUrl(string photoPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                       HttpContext.Current.Request.ApplicationPath.TrimEnd('/') +
                       "/photos/" + photoPath;
            }
            else
            {
                // Обробка випадку, коли HttpContext не доступний
                return "/photos/" + photoPath;
            }
        }
    }
}