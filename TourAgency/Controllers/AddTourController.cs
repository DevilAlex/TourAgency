using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourAgency.Models;
using Microsoft.EntityFrameworkCore;

namespace TourAgency.Controllers
{
    public class AddTourController : Controller
    {
        private DBContext db;
        private static SelectList Countries;
        private static SelectList TourOpers;
        private static SelectList Transport;
        public AddTourController(DBContext context)
        {
            db = context;
            Countries = new SelectList(db.Countries.Select(c => c.CountryName));
            TourOpers = new SelectList(db.TourOperators.Select(t => t.TourOperatorName));
            Transport = new SelectList(db.Transports.Select(t => t.TransportType));
        }
        [HttpGet]
        public ViewResult AddTourInfo()
        {
            ViewBag.Countries = Countries;
            ViewBag.TourOpers = TourOpers;
            ViewBag.Transport = Transport;
            ViewBag.HotelNames = new SelectList(from hotel in db.Hotels
                                                join city in db.Cities on hotel.CityID equals city.CityID
                                                join country in db.Countries on city.CountryID equals country.CountryID
                                                where country.CountryName == Countries.First().Text
                                                select hotel.HotelName);
            return View("AddTour");
        }
        [HttpPost]
        public IActionResult AddTourInfo(string Country, int Index)
        {
            ViewBag.HotelNames = new SelectList(from hotel in db.Hotels
                                                join city in db.Cities on hotel.CityID equals city.CityID
                                                join country in db.Countries on city.CountryID equals country.CountryID
                                                where country.CountryName == Country
                                                select hotel.HotelName);
            Countries.ElementAt(Index).Selected = true;
            ViewBag.Countries = Countries;
            ViewBag.TourOpers = TourOpers;
            ViewBag.Transport = Transport;
            return View("AddTour");
        }
        [HttpPost]
        public async Task<IActionResult> AddTour(AddTourViewModel tourmodel)
        {
            Tour tour = new Tour();
            tour.StartData = tourmodel.Data;
            tour.DaysOfRest = tourmodel.Days;
            tour.HotelID = db.Hotels.Where(h => h.HotelName == tourmodel.Hotel).Select(i => i.HotelID).First();
            tour.TourCost = (decimal)(db.Hotels.Where(h => h.HotelID == tour.HotelID).Select(c => c.RoomCost).First()) * tourmodel.Days;
            tour.TourOperatorID = db.TourOperators.Where(t => t.TourOperatorName == tourmodel.TourOperator).Select(p => p.TourOperatorID).First();
            tour.TransportID = db.Transports.Where(t => t.TransportType == tourmodel.Transport).Select(s => s.TransportID).First();
            tour.Avaliable = tourmodel.Avaliable;
            db.Tours.Add(tour);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
