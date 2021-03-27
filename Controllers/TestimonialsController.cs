using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyRent.Data;
using EasyRent.Models;
using System.IO;
using EasyRent.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace EasyRent.Controllers
{
    [Authorize]
    public class TestimonialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;
        public TestimonialsController(IHostingEnvironment env, ApplicationDbContext context)
        {
            _context = context;
            _env = env;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Testimonials.Include(t => t.User);

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var applicationDbContextx = _context.Testimonials.Include(t => t.User).Where(m => m.UserId.Equals(userId));
                return View(await applicationDbContextx.ToListAsync());
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }

            return View(testimonials);
        }
        [Authorize(Roles = "Admin, Agent")]
        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("Id,Testimony,UserId,isApproved,UploadedFile,ImageName")] Testimonials testimonials)
        {

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                testimonials.UserId = userId;
                testimonials.isApproved = false;
                testimonials.ImageName = "not available";
            }
            try {




                try
                {
                    var iscorrectformat = false;
                    string uniqueName = null;
                    string filePath = null;
                    FileInfo fi = new FileInfo(testimonials.UploadedFile.FileName);

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
                        return View(testimonials);
                    }

                    if (testimonials.UploadedFile != null)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + testimonials.UploadedFile.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueName);
                        testimonials.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        testimonials.ImageName = uniqueName;
                    }
                }
                catch
                {
                    return View(testimonials);
                }
                _context.Add(testimonials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", testimonials.UserId);
            return View(testimonials);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Show(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Testimonials.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            @property.isApproved = true;
            _context.Testimonials.Update(@property);
            _context.SaveChanges();
            var applicationDbContext = _context.Testimonials.Include(t => t.User);

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var applicationDbContextx = _context.Testimonials.Include(t => t.User).Where(m => m.UserId.Equals(userId));
                return View(await applicationDbContextx.ToListAsync());
            }
            return View("Index",await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnShow(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Testimonials.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            @property.isApproved = false;
            _context.Testimonials.Update(@property);
            _context.SaveChanges();
            var applicationDbContext = _context.Testimonials.Include(t => t.User);

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var applicationDbContextx = _context.Testimonials.Include(t => t.User).Where(m => m.UserId.Equals(userId));
                return View(await applicationDbContextx.ToListAsync());
            }
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Testimonials/Edit/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials.FindAsync(id);
            if (testimonials == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", testimonials.UserId);
            return View(testimonials);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Testimony,UserId,isApproved,UploadedFile,ImageName")] Testimonials testimonials)
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                testimonials.UserId = userId;
                testimonials.isApproved = false;
                testimonials.ImageName = "not available";
            }
            if (id != testimonials.Id)
            {
                return NotFound();
            }

           try
            {
                try
                {


                    if (testimonials.UploadedFile != null)
                    {
                        try
                        {
                            var iscorrectformat = false;
                            string uniqueName = null;
                            string filePath = null;
                            FileInfo fi = new FileInfo(testimonials.UploadedFile.FileName);

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
                                return View(testimonials);
                            }

                            if (testimonials.UploadedFile != null)
                            {
                                string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                                uniqueName = Guid.NewGuid().ToString() + "_" + testimonials.UploadedFile.FileName;
                                filePath = Path.Combine(uploadsFolder, uniqueName);
                                testimonials.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                                testimonials.ImageName = uniqueName;
                            }
                        }
                        catch
                        {
                            return View(testimonials);
                        }
                    }
                   
                    _context.Update(testimonials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialsExists(testimonials.Id))
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
            catch
            {

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", testimonials.UserId);
            return View(testimonials);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }

            return View(testimonials);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonials = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialsExists(int id)
        {
            return _context.Testimonials.Any(e => e.Id == id);
        }
    }
}
