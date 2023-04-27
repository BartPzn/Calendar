using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Data;
using Calendar.Models;
using Calendar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public IActionResult Index()
        {
            var reservations = _context.Reservations
            .Select(r => new ReservationViewModel
            {
                Id = r.Id,
                ApartmentName = r.Apartment.ApartmentName,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate
            })
            .ToList();
            return View(reservations);
            
        }
        public IActionResult Calendar()
        {
            var reservations = _context.Reservations
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    ApartmentName = r.Apartment.ApartmentName,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate
                })
                .ToList();

            var model = new List<Reservation>();

            foreach (var r in reservations)
            {
                model.Add(new Reservation
                {
                    Id = r.Id,
                    Apartment = new Apartment { ApartmentName = r.ApartmentName },
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate
                });
            }

            return View(model);
        }

        // GET: Reservations/Create
        [HttpGet]
        public async Task<string> GetApartmentName(int apartmentName)
        {
            var apartment = await _context.Apartments.FindAsync(apartmentName);
            return apartment?.ApartmentName ?? "";
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentName,CheckInDate,CheckOutDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ApartmentName = new SelectList(_context.Apartments, "Id", "ApartmentName", reservation.Apartment);
            return View(reservation);
        }

        public IActionResult Create()
        {
            ViewBag.Apartment = new SelectList(_context.Apartments, "Id", "ApartmentName");
            return View();
        }
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var model = new ReservationViewModel
      

            {
                Id = reservation.Id,
                ApartmentName = reservation.Apartment.ApartmentName,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var reservation = await _context.Reservations.FindAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                reservation.Apartment.ApartmentName = model.Apartment.ApartmentName;
                reservation.CheckInDate = model.CheckInDate;
                reservation.CheckOutDate = model.CheckOutDate;

                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }


        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
