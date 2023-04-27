using System;
using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string ApartmentName { get; set; }

        public virtual Apartment Apartment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public bool IsValid()
        {
            return CheckInDate < CheckOutDate;
        }
    }

}
