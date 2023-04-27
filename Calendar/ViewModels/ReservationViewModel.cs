using System.ComponentModel.DataAnnotations;
using Calendar.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Calendar.Models.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            ApartmentName = "";
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa apartamentu jest wymagana.")]
        [Display(Name = "Nazwa apartamentu")]
        public string ApartmentName { get; set; }

        [Required(ErrorMessage = "Data zameldowania jest wymagana.")]
        [Display(Name = "Data zameldowania")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Data wymeldowania jest wymagana.")]
        [Display(Name = "Data wymeldowania")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public List<Apartment> Apartments { get; set; }



    }
}
