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
        public async Task<IActionResult> Buy(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var books = from b in _context.Books
                         select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                || b.Genre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Genre":
                    books = books.OrderBy(s => s.Genre);
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(s => s.Genre);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), page ?? 1, pageSize));
            //return View(await _context.Books.ToListAsync());
        }
        //Get: Details
        public async Task<IActionResult> BookDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .SingleOrDefaultAsync(m => m.BookID == id);
            HttpContext.Session.SetString("CartBookTitle", book.Title);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Cart()
        {
            //int? id = HttpContext.Session.GetInt32("PatronID");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //ShoppingCart shoppingcart[] = await _context.ShoppingCarts
            //    .Select(sc => sc.PatronID == id);
            return View( _context.ShoppingCarts.Where(t => t.PatronID.Equals(HttpContext.Session.GetInt32("PatronID"))));
            //return View(await _context.ShoppingCarts.ToListAsync());
        }

        public async Task<IActionResult> AddToCart()
        {

            string id2 = HttpContext.Session.GetString("CartBookTitle");
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Title == id2);
            ShoppingCart Cart = new ShoppingCart();
            var bookID = book.BookID;
            Cart.BookID = book.BookID;
            Cart.Price = book.Price;
            Cart.Title = book.Title;
            Cart.Qty = 1;
            Cart.Path = book.Path;
            Cart.PatronID = HttpContext.Session.GetInt32("PatronID") ?? default(int);
            if (ModelState.IsValid)
            {
                _context.Add(Cart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cart = await _context.ShoppingCarts.SingleOrDefaultAsync(m => m.ID == id);
            _context.ShoppingCarts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart2(int id)
        {
            var cart = await _context.ShoppingCarts.SingleOrDefaultAsync(m => m.ID == id);
            _context.ShoppingCarts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("AddToCartPost")]
        //public async Task<IActionResult> AddToCart2()
        //{
        //    int? id1 = HttpContext.Session.GetInt32("CartBookId");
        //    int id2 = id1 ?? default(int);
        //    var book = await _context.Books.SingleOrDefaultAsync(m => m.BookID == id2);
        //    ShoppingCart Cart = new ShoppingCart();
        //    Cart.BookID = book.BookID;
        //    Cart.Price = book.Price;
        //    Cart.Title = book.Title;
        //    Cart.Qty = 1;
        //    Cart.Path = book.Path;
        //    Cart.PatronID = HttpContext.Session.GetInt32("PatronId") ?? default(int);
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(Cart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Cart");
        //    }
        //    return RedirectToAction("Error");
        //}

        public IActionResult Checkout()
        {
            return View(_context.ShoppingCarts.Where(t => t.PatronID.Equals(HttpContext.Session.GetInt32("PatronID"))));

        }
        public IActionResult Confirmation()
        {
            return View(_context.ShoppingCarts.Where(t => t.PatronID.Equals(HttpContext.Session.GetInt32("PatronID"))));

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
            HttpContext.Session.SetInt32("PatronID", DataPatron.PatronID);
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