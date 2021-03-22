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
    public class PropertyModesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyModesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyModes
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "Rent";
            return View(await _context.propertyModes.ToListAsync());
        }

        // GET: PropertyModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Rent";
            if (id == null)
            {
                return NotFound();
            }

            var propertyMode = await _context.propertyModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyMode == null)
            {
                return NotFound();
            }

            return View(propertyMode);
        }

        // GET: PropertyModes/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Rent";
            return View();
        }

        // POST: PropertyModes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PropertyMode propertyMode)
        {
            ViewBag.NavigatedTO = "Rent";
            if (ModelState.IsValid)
            {
                _context.Add(propertyMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyMode);
        }

        // GET: PropertyModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Rent";
            if (id == null)
            {
                return NotFound();
            }

            var propertyMode = await _context.propertyModes.FindAsync(id);
            if (propertyMode == null)
            {
                return NotFound();
            }
            return View(propertyMode);
        }

        // POST: PropertyModes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PropertyMode propertyMode)
        {
            ViewBag.NavigatedTO = "Rent";
            if (id != propertyMode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyModeExists(propertyMode.Id))
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
            return View(propertyMode);
        }

        // GET: PropertyModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Rent";
            if (id == null)
            {
                return NotFound();
            }

            var propertyMode = await _context.propertyModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyMode == null)
            {
                return NotFound();
            }

            return View(propertyMode);
        }

        // POST: PropertyModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Rent";
            var propertyMode = await _context.propertyModes.FindAsync(id);
            _context.propertyModes.Remove(propertyMode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyModeExists(int id)
        {
            ViewBag.NavigatedTO = "Rent";
            return _context.propertyModes.Any(e => e.Id == id);
        }
    }
}
