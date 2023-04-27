using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Calendar.Models
{
    public class Apartment
    {
        public int Id { get; set; }


        [MaxLength(250)]
        public string ApartmentName { get; set; }

        
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public bool Available(DateTime checkInDate, DateTime checkOutDate)
        {
            return !Reservations.Any(r =>
                (checkInDate >= r.CheckInDate && checkInDate <= r.CheckOutDate) ||
                (checkOutDate >= r.CheckInDate && checkOutDate <= r.CheckOutDate) ||
                (checkInDate < r.CheckInDate && checkOutDate > r.CheckOutDate));
        }
    }
}

