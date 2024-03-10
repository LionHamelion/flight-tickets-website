using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5
{
    [Serializable] // Серіалізуємо клас, щоб його можна було зберігати в сесії
    public class Ticket
    {
        public Ticket() {

        }

        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturnTicket { get; set; }
        public bool IsBusinessClass { get; set; }
        public bool IsAdult { get; set; }
        public bool Luggage { get; set; }
        public int AccompanyingAdult { get; set; }
        public int AccompanyingChild { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public int CalculatePrice()
        {
            int basePrice = GetBasePrice(DepartureCity, ArrivalCity);
            double finalPrice = basePrice;

            if (IsBusinessClass)
            {
                finalPrice *= 3; // Надбавка за бізнес-клас для основного пасажира
            }
            else
            {
                finalPrice = IsAdult? basePrice : 0.8 * basePrice; // Розрахунок базової ціни
            }

            // Додавання супроводжуючих осіб
            if (AccompanyingAdult > 0)
            {
                finalPrice += AccompanyingAdult * (IsBusinessClass ? 2.5 * basePrice : 0.9 * basePrice);
            }
            if (AccompanyingChild > 0)
            {
                finalPrice += AccompanyingChild * (IsBusinessClass ? 3 * 0.7 * basePrice : 0.7 * basePrice);
            }

            // Надбавка за багаж
            if (Luggage && !IsBusinessClass)
            {
                finalPrice *= 1.25;
            }

            // Перевірка на квиток в обидва кінці
            if (IsReturnTicket)
            {
                finalPrice *= 2;
            }

            return (int)Math.Round(finalPrice);
        }

        private int GetBasePrice(string departureCity, string arrivalCity)
        {
            string route = departureCity.ToLower() + "-" + arrivalCity.ToLower();

            switch (route)
            {
                case "київ-гамбург":
                case "гамбург-київ":
                    return 1000;
                case "київ-тбілісі":
                case "тбілісі-київ":
                    return 1500;
                case "київ-шарм-ель-шейх":
                case "шарм-ель-шейх-київ":
                    return 2000;
                case "гамбург-тбілісі":
                case "тбілісі-гамбург":
                    return 2500;
                case "гамбург-шарм-ель-шейх":
                case "шарм-ель-шейх-гамбург":
                    return 2500;
                case "тбілісі-шарм-ель-шейх":
                case "шарм-ель-шейх-тбілісі":
                    return 1500;
                default:
                    throw new ArgumentException("Невідомий маршрут");
            }
        }
        public void StoreInSession()
        {
            HttpContext.Current.Session["Ticket"] = this;
        }

        public static Ticket RetrieveFromSession()
        {
            return HttpContext.Current.Session["Ticket"] as Ticket;
        }

    }
}