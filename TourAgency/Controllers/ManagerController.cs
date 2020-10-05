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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TourAgency.Controllers
{
    public class ManagerController : Controller
    {
        private DBContext db;
        private static SelectList Countries;
        public ManagerController(DBContext context)
        {
            db = context;
            Countries = new SelectList(db.Countries.Select(c => c.CountryName));
            
        }
        public async Task<IActionResult> ManagersAll()
        {
            var viewmodel = from m in db.Managers
                            join c in db.Countries on m.CountryID equals c.CountryID
                            join d in db.Deals on m.ManagerID equals d.ManagerID
                            join t in db.Tours on d.TourID equals t.TourID
                            select new ManagerViewModel
                            {
                                ManagerID = m.ManagerID,
                                FirstName = m.ManagerName,
                                LastName = m.ManagerSurname,
                                Prime = m.Prime,
                                CountryName = c.CountryName,
                                CountTours = 1,
                                SumCost = t.TourCost
                            } into Table
                            group Table by new { Table.ManagerID, Table.LastName, Table.FirstName, Table.CountryName, Table.Prime } into g
                            select new ManagerViewModel
                            {
                                ManagerID = g.Key.ManagerID,
                                FirstName = g.Key.LastName,
                                LastName = g.Key.FirstName,
                                Prime = g.Key.Prime,
                                CountryName = g.Key.CountryName,
                                CountTours = g.Sum(arg => arg.CountTours),
                                SumCost = g.Sum(arg => arg.SumCost)
                            };

            return View(await viewmodel.ToListAsync());
        }
        public ViewResult ManagerAdd()
        {
            ViewBag.Countries = Countries;
            Countries.ElementAt(0).Selected = true;
            return View();
        }

        [HttpPost]
        public IActionResult ManagerAdd(Manager manager)
        {
            ViewBag.Countries = Countries;
            Countries.ElementAt(0).Selected = true;
            Manager user = db.Managers.FirstOrDefault(arg => arg.ManagerName == manager.ManagerName && arg.ManagerSurname == manager.ManagerSurname && arg.ManagerLogin == manager.ManagerLogin);
            if (user != null)
                ModelState.AddModelError("", "Данный менеджер уже есть в базе");
            if (ModelState.IsValid)
            {
                manager.CountryID = db.Countries.Where(c => c.CountryName == manager.CountryID).Select(c => c.CountryID).First();
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("ManagersAll");
            }
            return View();
        }
            
        public IActionResult ManagerEdit(int manID)
        {
            if (manID == 0)
            {
                ModelState.AddModelError("", "Не выбран менеджер");
                return RedirectToAction("ManagersAll");
            }
            var manager = db.Managers.Find(manID);
            var country = db.Countries.Find(manager.CountryID);
            manager.CountryID = country.CountryName;
            var temp = db.Countries.Select(c => c.CountryName).ToList();
            var Index = temp.FindIndex(arg => string.Equals(arg, country.CountryName));
            Countries.ElementAt(Index).Selected = true;
            ViewBag.Countries = Countries;
            return View(manager);
        }
        [HttpPost]
        public async Task<IActionResult> ManagerEdit(Manager manager)
        {
            ViewBag.Countries = Countries;
            if (ModelState.IsValid)
            {
                var oldmanager = db.Managers.Find(manager.ManagerID);
                oldmanager.ManagerName = manager.ManagerName;
                oldmanager.ManagerSurname = manager.ManagerSurname;
                oldmanager.ManagerLogin = manager.ManagerLogin;
                oldmanager.Prime = manager.Prime;
                oldmanager.CountryID = db.Countries.Where(c => c.CountryName == manager.CountryID).Select(c => c.CountryID).First();
                db.Managers.Update(oldmanager);
                await db.SaveChangesAsync();
                return RedirectToAction("ManagersAll");
            }
            else
                return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ManagerDel(int manID)
        {
            if (manID == 0)
            {
                ModelState.AddModelError("", "Не выбран менеджер");
                return RedirectToAction("ManagersAll");
            }
            Manager manager = db.Managers.First(p => p.ManagerID == manID);
            db.Managers.Remove(manager);
            await db.SaveChangesAsync();
            return RedirectToAction("ManagersAll");
        }
    }
}
