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
    public class PropertyTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyTypesController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: PropertyTypes
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "Type";
            return View(await _context.PropertyTypes.ToListAsync());
        }

        // GET: PropertyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Type";
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await _context.PropertyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        // GET: PropertyTypes/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Type";
            return View();
        }

        // POST: PropertyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PropertyType propertyType)
        {
            ViewBag.NavigatedTO = "Type";
            if (ModelState.IsValid)
            {
                _context.Add(propertyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyType);
        }

        // GET: PropertyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Type";
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await _context.PropertyTypes.FindAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }
            return View(propertyType);
        }

        // POST: PropertyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PropertyType propertyType)
        {
            ViewBag.NavigatedTO = "Type";
            if (id != propertyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyTypeExists(propertyType.Id))
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
            return View(propertyType);
        }

        // GET: PropertyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Type";
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await _context.PropertyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        // POST: PropertyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Type";
            var propertyType = await _context.PropertyTypes.FindAsync(id);
            _context.PropertyTypes.Remove(propertyType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyTypeExists(int id)
        {
            ViewBag.NavigatedTO = "Type";
            return _context.PropertyTypes.Any(e => e.Id == id);
        }
    }
}
