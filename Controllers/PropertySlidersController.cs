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
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using System.IO;
using EasyRent.Constants;

namespace EasyRent.Controllers
{
    [Authorize(Roles = "Admin, Agent")]
    public class PropertySlidersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public PropertySlidersController(IHostingEnvironment env, ApplicationDbContext context)
        {
            _context = context;
            _env = env;
        }

        // GET: PropertySliders
        public async Task<IActionResult> Index()
        {

            ViewBag.NavigatedTO = "Slider";
            var applicationDbContext = _context.PropertySliders.Include(p => p.Properties);

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var applicationDbContextx = _context.PropertySliders.Include(p => p.Properties).Where(m => m.Properties.UserId.Equals(userId));
                return View(await applicationDbContextx.ToListAsync());
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertySliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Slider";
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
            ViewBag.NavigatedTO = "Slider";
            ViewData["PropertiesId"] = new SelectList(_context.Properties, "Id", "Address");
            return View();
        }

        // POST: PropertySliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageName,PropertiesId,UploadedFile")] PropertySlider propertySlider)
        {



            ViewBag.NavigatedTO = "Slider";



            if (ModelState.IsValid)
            {

                try
                {
                    var iscorrectformat = false;
                    string uniqueName = null;
                    string filePath = null;
                    FileInfo fi = new FileInfo(propertySlider.UploadedFile.FileName);


                    var actualextension = fi.Extension;
                    var imageextensions = FileFormat.GetSupportedImageTypeExtensionsList();
                    foreach (var imageExtension in imageextensions)
                    {
                        if (imageExtension == actualextension)
                        {
                            iscorrectformat = true;
                        }
                    }
                    if (iscorrectformat == false)
                    {
                        return View(propertySlider);
                    }

                    if (propertySlider.UploadedFile != null)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + propertySlider.UploadedFile.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueName);
                        propertySlider.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        propertySlider.ImageName = uniqueName;
                    }
                }
                catch
                {

                }
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
            ViewBag.NavigatedTO = "Slider";
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageName,PropertiesId,UploadedFile")] PropertySlider propertySlider)
        {
            ViewBag.NavigatedTO = "Slider";
            if (id != propertySlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    if (propertySlider.UploadedFile != null)
                    {

                        try
                        {
                            var iscorrectformat = false;
                            string uniqueName = null;
                            string filePath = null;
                            FileInfo fi = new FileInfo(propertySlider.UploadedFile.FileName);

                            var actualextension = fi.Extension;
                            var imageextensions = FileFormat.GetSupportedImageTypeExtensionsList();
                            foreach (var imageExtension in imageextensions)
                            {
                                if (imageExtension == actualextension)
                                {
                                    iscorrectformat = true;
                                }
                            }
                            if (iscorrectformat == false)
                            {
                                return View(propertySlider);
                            }
                            if (propertySlider.UploadedFile != null)
                            {
                                string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                                uniqueName = Guid.NewGuid().ToString() + "_" + propertySlider.UploadedFile.FileName;
                                filePath = Path.Combine(uploadsFolder, uniqueName);
                                propertySlider.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                                propertySlider.ImageName = uniqueName;
                            }
                        }
                        catch
                        {

                        }

                    }
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
            ViewBag.NavigatedTO = "Slider";
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
            ViewBag.NavigatedTO = "Slider";
            var propertySlider = await _context.PropertySliders.FindAsync(id);
            _context.PropertySliders.Remove(propertySlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertySliderExists(int id)
        {
            ViewBag.NavigatedTO = "Slider";
            return _context.PropertySliders.Any(e => e.Id == id);
        }
    }
}
