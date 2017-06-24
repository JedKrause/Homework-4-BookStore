using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework_4_BookStore.Data;
using Homework_4_BookStore.Models.ViewModels;

namespace Homework_4_BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            IQueryable<RentalCountGroup> data =
            from Rentals in _context.Rentals
            group Rentals by Rentals.BookID into intgroup
            select new RentalCountGroup()
            {
                BookID = intgroup.Key,
                RentalID = intgroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
