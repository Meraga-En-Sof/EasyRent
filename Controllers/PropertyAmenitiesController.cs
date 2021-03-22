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
    [Authorize(Roles = "Admin, Team, Agent")]
    public class PropertyAmenitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyAmenitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyAmenities
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "Amenities";
            var applicationDbContext = _context.PropertyAmenities.Include(p => p.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyAmenities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Amenities";
            if (id == null)
            {
                return NotFound();
            }

            var propertyAmenities = await _context.PropertyAmenities
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyAmenities == null)
            {
                return NotFound();
            }

            return View(propertyAmenities);
        }

        // GET: PropertyAmenities/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Amenities";
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address");
            return View();
        }

        // POST: PropertyAmenities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PropertyId")] PropertyAmenities propertyAmenities)
        {
            ViewBag.NavigatedTO = "Amenities";
            if (ModelState.IsValid)
            {
                _context.Add(propertyAmenities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", propertyAmenities.PropertyId);
            return View(propertyAmenities);
        }

        // GET: PropertyAmenities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Amenities";
            if (id == null)
            {
                return NotFound();
            }

            var propertyAmenities = await _context.PropertyAmenities.FindAsync(id);
            if (propertyAmenities == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", propertyAmenities.PropertyId);
            return View(propertyAmenities);
        }

        // POST: PropertyAmenities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PropertyId")] PropertyAmenities propertyAmenities)
        {
            ViewBag.NavigatedTO = "Amenities";
            if (id != propertyAmenities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyAmenities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyAmenitiesExists(propertyAmenities.Id))
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
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", propertyAmenities.PropertyId);
            return View(propertyAmenities);
        }

        // GET: PropertyAmenities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Amenities";
            if (id == null)
            {
                return NotFound();
            }

            var propertyAmenities = await _context.PropertyAmenities
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyAmenities == null)
            {
                return NotFound();
            }

            return View(propertyAmenities);
        }

        // POST: PropertyAmenities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Amenities";
            var propertyAmenities = await _context.PropertyAmenities.FindAsync(id);
            _context.PropertyAmenities.Remove(propertyAmenities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyAmenitiesExists(int id)
        {
            ViewBag.NavigatedTO = "Amenities";
            return _context.PropertyAmenities.Any(e => e.Id == id);
        }
    }
}
