using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework_4_BookStore.Data;
using Homework_4_BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4_BookStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly LibraryContext _context;

        public ShopController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }

        //Get: Buy
        public async Task<IActionResult> Buy()
        {
            return View(await _context.Books.ToListAsync());
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Patron patron)
        {
            var DataPatron = _context.Patrons
                .Single(p => p.LastName == patron.LastName && p.Password == patron.Password);/*.Where(p => patron.LastName == p.LastName && patron.Password == p.Password);*/
            HttpContext.Session.SetString("ValidUser", "true");
            HttpContext.Session.SetString("Username", DataPatron.FirstName + " " + DataPatron.LastName);
            HttpContext.Session.SetInt32("PermissionLevel", DataPatron.PermissionsLevel);
            return RedirectToAction("Buy");
            //if (ModelState.IsValid)
            //{
            //    Patron RetPatron = patron.GetPatron(patron.LastName, patron.Password);
            //    if (RetPatron.LastName == patron.LastName)
            //    {
            //        HttpContext.Session.SetString("ValidUser", "true");
            //        HttpContext.Session.SetString("Username", RetPatron.FirstName + " " + RetPatron.LastName);
            //        HttpContext.Session.SetInt32("PermissionLevel", RetPatron.PermissionsLevel);
            //    }
                
            //    return RedirectToAction("Buy");
            //}
            //return View(patron);
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,MembershipDate,PermissionsLevel,Password")] Patron patron)
        {
            if (ModelState.IsValid)
            {
                patron.MembershipDate = DateTime.Now;
                patron.PermissionsLevel = 1;
                _context.Add(patron);
                HttpContext.Session.SetString("ValidUser", "true");
               HttpContext.Session.SetString("Username", patron.FirstName + " " + patron.LastName);
               HttpContext.Session.SetInt32("PermissionLevel", patron.PermissionsLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Buy");
            }
            return View(patron);
        }
    }
}