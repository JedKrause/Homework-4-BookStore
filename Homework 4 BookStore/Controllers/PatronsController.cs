using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_4_BookStore.Data;
using Homework_4_BookStore.Models;

namespace Homework_4_BookStore.Controllers
{
    public class PatronsController : Controller
    {
        private readonly LibraryContext _context;

        public PatronsController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Patrons
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var patron = from s in _context.Patrons
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                patron = patron.Where(s => s.LastName.Contains(searchString)
                || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    patron = patron.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    patron = patron.OrderBy(s => s.MembershipDate);
                    break;
                case "date_desc":
                    patron = patron.OrderByDescending(s => s.MembershipDate);
                    break;
                default:
                    patron = patron.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Patron>.CreateAsync(patron.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Patrons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patron = await _context.Patrons
                .Include(s => s.Rentals)
                .ThenInclude(e => e.Book)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.PatronID == id);
            if (patron == null)
            {
                return NotFound();
            }

            return View(patron);
        }

        // GET: Patrons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patrons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatronID,LastName,FirstName,MembershipDate")] Patron patron)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patron);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patron);
        }

        // GET: Patrons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patron = await _context.Patrons.SingleOrDefaultAsync(m => m.PatronID == id);
            if (patron == null)
            {
                return NotFound();
            }
            return View(patron);
        }

        // POST: Patrons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatronID,LastName,FirstName,MembershipDate")] Patron patron)
        {
            if (id != patron.PatronID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patron);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatronExists(patron.PatronID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(patron);
        }

        // GET: Patrons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patron = await _context.Patrons
                .SingleOrDefaultAsync(m => m.PatronID == id);
            if (patron == null)
            {
                return NotFound();
            }

            return View(patron);
        }

        // POST: Patrons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patron = await _context.Patrons.SingleOrDefaultAsync(m => m.PatronID == id);
            _context.Patrons.Remove(patron);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PatronExists(int id)
        {
            return _context.Patrons.Any(e => e.PatronID == id);
        }
    }
}
