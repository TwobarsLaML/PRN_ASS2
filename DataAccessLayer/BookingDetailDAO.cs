using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Data.SqlClient;


namespace DataAccessLayer
{
    public class BookingDetailDAO
    {
        private FuminiHotelManagementContext db = new FuminiHotelManagementContext();
        public List<BookingDetail> GetAllBookingDetail()
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {

                bookingDetails = db.BookingDetails
                    .Select(b => b)
                    .ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot find booking details");
            }
            return bookingDetails;
        }
           
    }
}
