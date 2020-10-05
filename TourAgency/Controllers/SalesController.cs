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
    public class SalesController : Controller
    {
        private DBContext db;
        private static SelectList Days;
        private static SelectList Clients;
        private static SelectList Managers;
        private static SelectList Countries;
        private static SelectList TourOpers;
        public SalesController(DBContext context)
        {
            db = context;
            var DaysItems = db.Tours.Select(arg => arg.DaysOfRest).Distinct().ToList();
            DaysItems.Insert(0,0);
            Days = new SelectList(DaysItems);
            var ClientsItems = db.Clients.Select(arg => arg.ClientName + " " + arg.ClientSurname).Distinct().ToList();
            ClientsItems.Insert(0, " ");
            Clients = new SelectList(ClientsItems);
            var ManagersItems = db.Managers.Select(arg => arg.ManagerName + " " + arg.ManagerSurname).Distinct().ToList();
            ManagersItems.Insert(0, " ");
            Managers = new SelectList(ManagersItems);
            var CountryItems = db.Countries.Select(arg => arg.CountryName).ToList();
            CountryItems.Insert(0, " ");
            Countries = new SelectList(CountryItems);
            var TourOperItems = db.TourOperators.Select(arg => arg.TourOperatorName).Distinct().ToList();
            TourOperItems.Insert(0, " ");
            TourOpers = new SelectList(TourOperItems);
        }
        public async Task<IActionResult> Sales()
        {
            var sales = await (from country in db.Countries
                                join citi in db.Cities on country.CountryID equals citi.CountryID
                                join hotel in db.Hotels on citi.CityID equals hotel.CityID
                                join tur in db.Tours on hotel.HotelID equals tur.HotelID
                                join deal in db.Deals on tur.TourID equals deal.TourID
                                join client in db.Clients on deal.ClientID equals client.ClientID
                                join manager in db.Managers on deal.ManagerID equals manager.ManagerID
                                join turoper in db.TourOperators on tur.TourOperatorID equals turoper.TourOperatorID
                                orderby tur.DaysOfRest ascending
                                select new SalesViewModel
                                {
                                    ClientName = client.ClientName + " " + client.ClientSurname,
                                    ManagerName = manager.ManagerName + " " + manager.ManagerSurname,
                                    Days = tur.DaysOfRest,
                                    Country = country.CountryName,
                                    TourOper = turoper.TourOperatorName,
                                    DateIn = deal.AgreementDate,
                                    Cost = tur.TourCost,
                                    AgrNumber = deal.AgreementNumber,
                                    City = citi.CityName,
                                    Hotel = hotel.HotelName,
                                    DateOut = tur.StartData,
                                }).ToListAsync();
            for (int i = 0; i < sales.Count(); i++)
                sales[i].Index = i+1;
            ViewBag.Days = Days;
            ViewBag.Clients = Clients;
            ViewBag.Managers = Managers;
            ViewBag.Countries = Countries;
            ViewBag.TourOpers = TourOpers;
            return View(sales);
        }
    }
}
