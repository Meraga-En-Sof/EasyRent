using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyRent.Data;
using EasyRent.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyRent.Controllers
{
    [Authorize(Roles = "Admin, Team")]
    public class OurServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OurServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OurServices
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "Services";
            return View(await _context.OurServices.ToListAsync());
        }

        // GET: OurServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Services";
            if (id == null)
            {
                return NotFound();
            }

            var ourService = await _context.OurServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ourService == null)
            {
                return NotFound();
            }

            return View(ourService);
        }

        // GET: OurServices/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Services";
            return View();
        }

        // POST: OurServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,IconName,Content")] OurService ourService)
        {
            ViewBag.NavigatedTO = "Services";
            if (ModelState.IsValid)
            {
                _context.Add(ourService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ourService);
        }

        // GET: OurServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Services";
            if (id == null)
            {
                return NotFound();
            }

            var ourService = await _context.OurServices.FindAsync(id);
            if (ourService == null)
            {
                return NotFound();
            }
            return View(ourService);
        }

        // POST: OurServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IconName,Content")] OurService ourService)
        {
            ViewBag.NavigatedTO = "Services";
            if (id != ourService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ourService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OurServiceExists(ourService.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ourService);
        }

        // GET: OurServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Services";
            if (id == null)
            {
                return NotFound();
            }

            var ourService = await _context.OurServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ourService == null)
            {
                return NotFound();
            }

            return View(ourService);
        }

        // POST: OurServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Services";
            var ourService = await _context.OurServices.FindAsync(id);
            _context.OurServices.Remove(ourService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OurServiceExists(int id)
        {
            ViewBag.NavigatedTO = "Services";
            return _context.OurServices.Any(e => e.Id == id);
        }
    }
}
