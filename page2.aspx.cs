using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Lab5
{
    public partial class page2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && NavigationManager.ValidateReferrer(Session, "page2.aspx"))
            {
                Session["ExpectedReferrer"] = "page2.aspx";
                LoadCities();
            }
        }
        private void LoadCities()
        {
            string filePath = Server.MapPath("cities.json");
            string json = File.ReadAllText(filePath);
            var serializer = new JavaScriptSerializer();
            var cities = serializer.Deserialize<string[]>(json);

            DepartureCity.Items.Clear();
            ArrivalCity.Items.Clear();

            foreach (string city in cities)
            {
                DepartureCity.Items.Add(new ListItem(city));
                ArrivalCity.Items.Add(new ListItem(city));
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page1.aspx";
            Response.Redirect("page1.aspx");
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            
            string name = NameTextBox.Text;
            string photoPath = HandlePhoto();
            string departureCity = DepartureCity.Text;
            string arrivalCity = ArrivalCity.Text;
            bool isReturnTicket = false;
            DateTime flightDate = Convert.ToDateTime(FlightDate.Text);
            DateTime? returnDate;
            try
            {
                returnDate = Convert.ToDateTime(ReturnDate.Text);
                isReturnTicket = true;
            }
            catch
            {
                returnDate = null;
            }
            
            Ticket ticket = new Ticket()
            {
                Name = name,
                PhotoPath = photoPath,
                DepartureCity = departureCity,
                ArrivalCity = arrivalCity,
                FlightDate = flightDate,
                IsReturnTicket = isReturnTicket,
                ReturnDate = returnDate
            };
            ticket.StoreInSession();
            Thread.Sleep(2000);
            Session["ExpectedReferrer"] = "page3.aspx";
            Response.Redirect("page3.aspx");
        }
        private string HandlePhoto()
        {
            if (PhotoUpload.HasFile)
            {
                try
                {
                    // Генерація унікального ідентифікатора для імені файлу
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUpload.FileName);

                    // Визначення шляху до папки 'photos'
                    string photosPath = Path.Combine(Server.MapPath("~/"), "photos");

                    // Створення папки 'photos', якщо вона не існує
                    if (!Directory.Exists(photosPath))
                    {
                        Directory.CreateDirectory(photosPath);
                    }

                    // Створення шляху до файлу
                    string filePath = Path.Combine(photosPath, uniqueFileName);

                    // Збереження файлу
                    PhotoUpload.SaveAs(filePath);
                    return uniqueFileName;
                    // Тут ви можете додати логіку для збереження шляху до файлу в базі даних або сесії, якщо це необхідно
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occured while saving photo: {ex}");
                }
            }
            else
            {
                throw new Exception("Photo not uploaded");
            }
        }
    }
}