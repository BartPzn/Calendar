using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Calendar.Models;
using Calendar.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private List<ReservationViewModel> _reservationViewModels = new List<ReservationViewModel>();

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var reservations = _context.Reservations.Include(r => r.Apartment).ToList();

            var reservationViewModels = reservations.Select(r => new ReservationViewModel
            {
                Id = r.Id,
                ApartmentName = r.Apartment.ApartmentName,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Apartments = new List<Apartment> { r.Apartment } // przypisz kolekcję zawierającą ten obiekt
            }).ToList();

            var reservationListViewModel = new ReservationListViewModel
            {
                Reservations = reservationViewModels
            };

            var homeViewModel = new HomeViewModel
            {
                ReservationListViewModel = reservationListViewModel
            };

            return View(reservationListViewModel);
        }

        public IActionResult Reservation(int id)
        {
            var apartment = _context.Apartments.FirstOrDefault(a => a.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            var reservation = new Reservation
            {
                Apartment = apartment,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };

            var viewModel = new ReservationViewModel
            {
                ApartmentName = apartment.ApartmentName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Apartments = new List<Apartment> { apartment } // przypisz kolekcję zawierającą ten obiekt
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reservation(ReservationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

           

            // Utwórz nową rezerwację z wartościami z ViewModel
            var reservation = new Reservation
            {
                
                CheckInDate = viewModel.CheckInDate,
                CheckOutDate = viewModel.CheckOutDate
            };

            // Dodaj rezerwację do bazy danych
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Reservations()
        {
            var reservations = _context.Reservations.Include(r => r.Apartment).ToList();
            var reservationViewModels = new List<ReservationViewModel>();
            foreach (var reservation in reservations)
            {
                var viewModel = new ReservationViewModel
                {
                    Id = reservation.Id,
                    ApartmentName = reservation.Apartment.ApartmentName,
                    CheckInDate = reservation.CheckInDate,
                    CheckOutDate = reservation.CheckOutDate
                };
                reservationViewModels.Add(viewModel);
            }

            var reservationListViewModel = new ReservationListViewModel
            {
                Reservations = reservationViewModels
            };

            return View(reservationListViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var viewModel = new ReservationViewModel();
            viewModel.Apartments = _context.Apartments.ToList();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
