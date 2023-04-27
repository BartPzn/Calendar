using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models.ViewModels
{
    public class HomeViewModel
    {
        public ReservationListViewModel Reservations { get; set; }
        public List<Apartment> Apartments { get; set; }
        public ReservationViewModel Reservation { get; set; }
        public ReservationListViewModel ReservationListViewModel { get; set; }
    }
}
