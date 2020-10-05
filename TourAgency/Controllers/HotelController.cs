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
    public class HotelController : Controller
    {
        private DBContext db;
        private static List<HotelViewModel> hotels;
        public HotelController(DBContext context)
        {
            db = context;
        }
        public ViewResult ChooseCountry()
        {
            List<string> Countries = (db.Countries.Select(c => c.CountryName)).ToList();
            return View(Countries);
        }
        [HttpGet]
        public ViewResult HotelInfo(string ChoosenCountry)
        {
            ViewBag.Title = "Все отели страны: " + ChoosenCountry;
            hotels = (from hotel in db.Hotels
                      join city in db.Cities on hotel.CityID equals city.CityID
                      join country in db.Countries on city.CountryID equals country.CountryID
                      join food in db.FoodTypes on hotel.FoodTypeID equals food.FoodTypeID
                      join rest in db.RestTypes on hotel.RestTypeID equals rest.RestTypeID
                      where country.CountryName == ChoosenCountry 
                      select new HotelViewModel
                      {
                          HotelName = hotel.HotelName,
                          CountryName = country.CountryName,
                          CityName = city.CityName,
                          Stars = hotel.Stars,
                          BuildYear = hotel.BuildYear,
                          RoomCost = (decimal)hotel.RoomCost,
                          HotelSquare = (decimal)hotel.HotelSquare,
                          FoodType = food.FoodTypeName,
                          RestType = rest.RestTypeName,
                          Description = hotel.Description
                      }).ToList();
            ViewBag.Index = 0;
            return View(hotels[ViewBag.Index]);
        }
        [HttpPost]
        public IActionResult HotelInfo(string submitButton, string CountryName, int Index)
        {
            ViewBag.Title = "Все отели страны: " + CountryName;
            if (submitButton == "next")
                Index = (Index < hotels.Count() - 1) ? (Index + 1) : 0;
            else
                Index = (Index == 0) ? (hotels.Count() - 1) : (Index - 1);
            ViewBag.Index = Index;
            return View(hotels[Index]);
        }
    }
}
       