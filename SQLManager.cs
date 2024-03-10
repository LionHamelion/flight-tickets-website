using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lab5
{
    public class SQLManager
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = Aero; Integrated Security = True; Trusted_Connection=Yes";
        public void AddTicket(Ticket ticket)
        {
            string query = "INSERT INTO Tickets (Name, PhotoPath, DepartureCity, ArrivalCity, FlightDate, ReturnDate, IsReturnTicket, IsBusinessClass, IsAdult, Luggage, AccompanyingAdult, AccompanyingChild, Price) VALUES (@Name, @PhotoPath, @DepartureCity, @ArrivalCity, @FlightDate, @ReturnDate, @IsReturnTicket, @IsBusinessClass, @IsAdult, @Luggage, @AccompanyingAdult, @AccompanyingChild, @Price)";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", ticket.Name);
                    cmd.Parameters.AddWithValue("@PhotoPath", ticket.PhotoPath);
                    cmd.Parameters.AddWithValue("@DepartureCity", ticket.DepartureCity);
                    cmd.Parameters.AddWithValue("@ArrivalCity", ticket.ArrivalCity);
                    cmd.Parameters.AddWithValue("@FlightDate", ticket.FlightDate);
                    cmd.Parameters.AddWithValue("@ReturnDate", ticket.ReturnDate.HasValue ? (object)ticket.ReturnDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsReturnTicket", ticket.IsReturnTicket);
                    cmd.Parameters.AddWithValue("@IsBusinessClass", ticket.IsBusinessClass);
                    cmd.Parameters.AddWithValue("@IsAdult", ticket.IsAdult);
                    cmd.Parameters.AddWithValue("@Luggage", ticket.Luggage);
                    cmd.Parameters.AddWithValue("@AccompanyingAdult", ticket.AccompanyingAdult);
                    cmd.Parameters.AddWithValue("@AccompanyingChild", ticket.AccompanyingChild);
                    cmd.Parameters.AddWithValue("@Price", ticket.Price);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTicketsByName(string name)
        {
            string query = "SELECT * FROM Tickets WHERE Name = @Name";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }
        public Ticket GetTicketById(int id)
        {
            string query = "SELECT * FROM Tickets WHERE Id = @Id";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            Ticket ticket = new Ticket()
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                PhotoPath = row["PhotoPath"].ToString(),
                DepartureCity = row["DepartureCity"].ToString(),
                ArrivalCity = row["ArrivalCity"].ToString(),
                FlightDate = Convert.ToDateTime(row["FlightDate"]),
                ReturnDate = row.IsNull("ReturnDate") ? (DateTime?)null : Convert.ToDateTime(row["ReturnDate"]),
                IsReturnTicket = Convert.ToBoolean(row["IsReturnTicket"]),
                IsBusinessClass = Convert.ToBoolean(row["IsBusinessClass"]),
                IsAdult = Convert.ToBoolean(row["IsAdult"]),
                Luggage = Convert.ToBoolean(row["Luggage"]),
                AccompanyingAdult = Convert.ToInt32(row["AccompanyingAdult"]),
                AccompanyingChild = Convert.ToInt32(row["AccompanyingChild"]),
                Price = Convert.ToInt32(row["Price"])
            };

            return ticket;
        }

        public void DeleteTicketById(int id)
        {
            string query = "DELETE FROM Tickets WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}