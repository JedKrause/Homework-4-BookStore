using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework_4_BookStore.Data;
using Homework_4_BookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

    }
}