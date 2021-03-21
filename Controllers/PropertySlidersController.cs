using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyRent.Data;
using EasyRent.Models;

namespace EasyRent.Controllers
{
    public class PropertySlidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertySlidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertySliders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertySliders.Include(p => p.Properties);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertySliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySlider = await _context.PropertySliders
                .Include(p => p.Properties)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertySlider == null)
            {
                return NotFound();
            }

            return View(propertySlider);
        }

        // GET: PropertySliders/Create
        public IActionResult Create()
        {
            ViewData["PropertiesId"] = new SelectList(_context.Properties, "Id", "Address");
            return View();
        }

        // POST: PropertySliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageName,PropertiesId")] PropertySlider propertySlider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertySlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertiesId"] = new SelectList(_context.Properties, "Id", "Address", propertySlider.PropertiesId);
            return View(propertySlider);
        }

        // GET: PropertySliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySlider = await _context.PropertySliders.FindAsync(id);
            if (propertySlider == null)
            {
                return NotFound();
            }
            ViewData["PropertiesId"] = new SelectList(_context.Properties, "Id", "Address", propertySlider.PropertiesId);
            return View(propertySlider);
        }

        // POST: PropertySliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageName,PropertiesId")] PropertySlider propertySlider)
        {
            if (id != propertySlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertySlider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertySliderExists(propertySlider.Id))
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
            ViewData["PropertiesId"] = new SelectList(_context.Properties, "Id", "Address", propertySlider.PropertiesId);
            return View(propertySlider);
        }

        // GET: PropertySliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySlider = await _context.PropertySliders
                .Include(p => p.Properties)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertySlider == null)
            {
                return NotFound();
            }

            return View(propertySlider);
        }

        // POST: PropertySliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertySlider = await _context.PropertySliders.FindAsync(id);
            _context.PropertySliders.Remove(propertySlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertySliderExists(int id)
        {
            return _context.PropertySliders.Any(e => e.Id == id);
        }
    }
}
