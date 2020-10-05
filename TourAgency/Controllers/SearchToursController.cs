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
    public class SearchToursController:Controller
    {
        private DBContext db;
        private SearchParamsViewModel viewmodel;
        private static SelectList Countries;
        public SearchToursController(DBContext context)
        {
            db = context;
            viewmodel = new SearchParamsViewModel();
            Countries = new SelectList(db.Countries.Select(arg => arg.CountryName));
            viewmodel.DateFrom = new DateTime(2018, 1, 1);
            viewmodel.DateTo = new DateTime(2019, 12, 31);
            viewmodel.DaysFrom = 6;
            viewmodel.DaysTo = 15;
            viewmodel.CostFrom = 1000;
            viewmodel.CostTo = 3200;
            viewmodel.Country = db.Countries.Select(c => c.CountryName).First();
            viewmodel.Star = new List<int>() { 1, 2, 3, 4, 5 };
            viewmodel.Food = db.FoodTypes.Select(arg => arg.FoodTypeID).ToList();
        }
        [HttpGet]
        public ViewResult SearchParams()
        {
            ViewBag.Countries = Countries;
            viewmodel.City = (from ci in db.Cities
                              join co in db.Countries on ci.CountryID equals co.CountryID
                              where co.CountryName == viewmodel.Country
                              select ci.CityName).ToList();
            return View(viewmodel);
        }
        [HttpPost]
        public IActionResult SearchParams(string Country, int Index)
        {
            viewmodel.City = (from ci in db.Cities
                              join co in db.Countries on ci.CountryID equals co.CountryID
                              where co.CountryName == Country
                              select ci.CityName).ToList();
            Countries.ElementAt(Index).Selected = true;
            ViewBag.Countries = Countries;
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Find(SearchParamsViewModel search)
        {
            var Input = await (from country in db.Countries
                               join citi in db.Cities on country.CountryID equals citi.CountryID
                               join hotel in db.Hotels on citi.CityID equals hotel.CityID
                               join food in db.FoodTypes on hotel.FoodTypeID equals food.FoodTypeID
                               join tur in db.Tours on hotel.HotelID equals tur.HotelID
                               join turoper in db.TourOperators on tur.TourOperatorID equals turoper.TourOperatorID
                               where country.CountryName == search.Country &&
                                     tur.StartData >= search.DateFrom && tur.StartData <= search.DateTo &&
                                     tur.DaysOfRest >= search.DaysFrom && tur.DaysOfRest <= search.DaysTo &&
                                     search.City.Any(arg => arg == citi.CityName) &&
                                     search.Food.Any(arg => arg == food.FoodTypeID) &&
                                     search.Star.Any(arg => arg == hotel.Stars) &&
                                     hotel.RoomCost >= search.CostFrom && hotel.RoomCost <= search.CostTo &&
                                     tur.Avaliable > 0
                               select new FindTourModelView
                               {
                                   TourID = tur.TourID,
                                   CountryID = country.CountryID,
                                   Country = country.CountryName,
                                   City = citi.CityName,
                                   Hotel = hotel.HotelName,
                                   DateIn = tur.StartData,
                                   Days = tur.DaysOfRest,
                                   Food = food.FoodTypeName,
                                   Stars = hotel.Stars,
                                   Cost = tur.TourCost,
                                   TourOper = turoper.TourOperatorName,
                                   Avaliable = tur.Avaliable
                               }).ToListAsync();
            for (var i = 1; i < Input.Count(); i++)
                Input[i].Index = i;
            return View(Input);
        }
        [HttpGet]
        public ViewResult BuyTour (int TourID, string CountryID)
        {
            BuyTourViewModel model = new BuyTourViewModel();
            model.TourID = TourID;
            model.CountryID = CountryID;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BuyTour(BuyTourViewModel model)
        {
            Client user = db.Clients.FirstOrDefault(arg => arg.ClientName == model.Name && arg.ClientSurname == model.Surname && arg.Birthday == model.Birthday);
            if (user == null)
                ModelState.AddModelError("", "Клиент отсутствует в базе");
            if (ModelState.IsValid)
            {
                Deal NewTour = new Deal();
                Random random = new Random();
                int number = random.Next(100);
                NewTour.ClientID = db.Clients.Where(c => c.ClientName == model.Name && c.ClientSurname == model.Surname && c.Birthday == model.Birthday).Select(c => c.ClientID).First();
                NewTour.ManagerID = db.Managers.Where(m => m.ManagerLogin == @User.Identity.Name).Select(m => m.ManagerID).First();
                NewTour.AgreementNumber = model.CountryID + number + NewTour.ClientID + NewTour.ManagerID;
                NewTour.AgreementDate = DateTime.Now;
                NewTour.TourID = model.TourID;
                db.Deals.Add(NewTour);
                await db.SaveChangesAsync();
                return RedirectToAction("SearchParams");
            }
            else
                return View();
        }
    }
}
