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
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Properties.Include(m => m.ClosedTo).Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(m => m.ClosedTo)
                .Include(m => m.PropertyMode)
                .Include(m => m.PropertyType)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["ClosedToId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PropertyModeId"] = new SelectList(_context.propertyModes, "Id", "Name");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,GoogleMapAddress,ImageName,Price,Description,Area,BedRooms,bathrooms,Garage,Stairs,BuildingConditon,FloorPlanImage,isDealClosed,isDisplayed,Country,CurrencyId,PropertyTypeId,PropertyModeId,UserId,VideoLink,ClosedToId")] Property @property)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClosedToId"] = new SelectList(_context.Users, "Id", "Id", @property.ClosedToId);
            ViewData["PropertyModeId"] = new SelectList(_context.propertyModes, "Id", "Name", @property.PropertyModeId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", @property.PropertyTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @property.UserId);
            return View(@property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["ClosedToId"] = new SelectList(_context.Users, "Id", "Id", @property.ClosedToId);
            ViewData["PropertyModeId"] = new SelectList(_context.propertyModes, "Id", "Name", @property.PropertyModeId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", @property.PropertyTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @property.UserId);
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,GoogleMapAddress,ImageName,Price,Description,Area,BedRooms,bathrooms,Garage,Stairs,BuildingConditon,FloorPlanImage,isDealClosed,isDisplayed,Country,CurrencyId,PropertyTypeId,PropertyModeId,UserId,VideoLink,ClosedToId")] Property @property)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.Id))
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
            ViewData["ClosedToId"] = new SelectList(_context.Users, "Id", "Id", @property.ClosedToId);
            ViewData["PropertyModeId"] = new SelectList(_context.propertyModes, "Id", "Name", @property.PropertyModeId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", @property.PropertyTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @property.UserId);
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(m => m.ClosedTo)
                .Include(m => m.PropertyMode)
                .Include(m => m.PropertyType)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Properties.FindAsync(id);
            _context.Properties.Remove(@property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
