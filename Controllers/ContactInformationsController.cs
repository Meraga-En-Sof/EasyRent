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
    public class ContactInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactInformations
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "ContactInfo";
            return View(await _context.ContactInformation.ToListAsync());

        }

        // GET: ContactInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // GET: ContactInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Email,PhoneNumber,GoogleMapPlugin")] ContactInformation contactInformation)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            if (ModelState.IsValid)
            {
                _context.Add(contactInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactInformation);
        }

        // GET: ContactInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation.FindAsync(id);
            if (contactInformation == null)
            {
                return NotFound();
            }
            return View(contactInformation);
        }

        // POST: ContactInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,Email,PhoneNumber,GoogleMapPlugin")] ContactInformation contactInformation)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            if (id != contactInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInformationExists(contactInformation.Id))
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
            return View(contactInformation);
        }

        // GET: ContactInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            if (id == null)
            {
                return NotFound();
            }

            var contactInformation = await _context.ContactInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // POST: ContactInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "ContactInfo";
            var contactInformation = await _context.ContactInformation.FindAsync(id);
            _context.ContactInformation.Remove(contactInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactInformationExists(int id)
        {
            return _context.ContactInformation.Any(e => e.Id == id);
        }
    }
}
