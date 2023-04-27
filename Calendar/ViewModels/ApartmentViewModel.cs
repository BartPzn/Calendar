using System;
using System.ComponentModel.DataAnnotations;

namespace Calendar.Models.ViewModels
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }

        public string ApartmentName { get; set; }
     
        [Range(1, Int32.MaxValue, ErrorMessage = "The Price field must be greater than 0.")]
        public decimal Price { get; set; }

        public List<ApartmentViewModel> Apartments { get; set; }
    }
}
