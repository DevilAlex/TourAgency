using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourAgency.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TourAgency.Controllers
{
    public class ClientController : Controller
    {
        private DBContext db;
        public ClientController(DBContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ClientsAll()
        {
            return View(await db.Clients.OrderBy(c => c.ClientSurname).ToListAsync());
        }
        public ViewResult ClientAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClientAdd(Client client)
        {
            Client user = db.Clients.FirstOrDefault(arg => arg.ClientName == client.ClientName && arg.ClientSurname == client.ClientSurname && arg.Birthday == client.Birthday);
            if (user != null)
                ModelState.AddModelError("", "Данный пользователь уже есть в базе");
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("ClientsAll");
            }
            else
                return View();
        }
        public IActionResult ClientEdit(int cliID)
        {
            if (cliID == 0)
            {
                ModelState.AddModelError("", "Не выбран клиент");
                return RedirectToAction("ClientsAll");
            }
            return View(db.Clients.Find(cliID));
        }
        [HttpPost]
        public async Task<IActionResult> ClientEdit(Client client)
        {
            if (ModelState.IsValid)
            {
                var oldclient = db.Clients.Find(client.ClientID);
                oldclient.ClientName = client.ClientName;
                oldclient.ClientSurname = client.ClientSurname;
                oldclient.Passport = client.Passport;
                oldclient.RusPassport = client.RusPassport;
                oldclient.WorkPlace = client.WorkPlace;
                oldclient.ManagerLogin = @User.Identity.Name;
                oldclient.EditDate = DateTime.Now;
                db.Clients.Update(oldclient);
                await db.SaveChangesAsync();
                return RedirectToAction("ClientsAll");
            }
            else
                return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ClientDel(int cliID)
        {
            if (cliID == 0)
            {
                ModelState.AddModelError("", "Не выбран клиент");
                return RedirectToAction("ClientsAll");
            }
            Client client = db.Clients.First(p => p.ClientID == cliID);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("ClientsAll");
        }
    }
}
