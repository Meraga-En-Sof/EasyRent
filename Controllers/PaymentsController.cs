using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyRent.Data;
using EasyRent.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using EasyRent.Constants;

namespace EasyRent.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public PaymentsController(IHostingEnvironment env, ApplicationDbContext context)
        {
            _context = context;
            _env = env;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            ViewBag.NavigatedTO = "Payment";
            var applicationDbContext = _context.Payments.Include(p => p.LandLord).Include(p => p.Property).Include(p => p.Tenant);
            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
               var  theapplicationDbContext = _context.Payments.Include(p => p.LandLord).Include(p => p.Property).Include(p => p.Tenant).Where(m =>m.LandLordId == userId || m.TenantId == userId);
                return View(await theapplicationDbContext.ToListAsync());


            }
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Payment";
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.LandLord)
                .Include(p => p.Property)
                .Include(p => p.Tenant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        [Authorize(Roles = "Admin")]
        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Payment";
            ViewData["LandLordId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address");
            ViewData["TenantId"] = new SelectList(_context.Users, "Id", "Id");
            return View();

        }

        [Authorize(Roles = "Tenant")]
        public IActionResult MakePayment(int Id)
        {
            ViewBag.NavigatedTO = "Payment";
            var property = _context.Properties.Where(m => m.Id == Id).FirstOrDefaultAsync();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payment = new Payments()
            {
                PropertyId = Id

            };
            
            return View(payment);
        }
        [Authorize(Roles = "Tenant")]
        [HttpPost]
        public ActionResult MakePayment(Payments payments)
        {
            
            ViewBag.NavigatedTO = "Payment";
            var property = _context.Properties.Where(m => m.Id == payments.PropertyId).FirstOrDefault();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            payments.LandLordId = property.UserId;
            payments.TenantId = userId;
            try
            {

                var iscorrectformat = false;
                string uniqueName = null;
                string filePath = null;
                FileInfo fi = new FileInfo(payments.UploadedFile.FileName);


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
                    return View(payments);
                }

                if (payments.UploadedFile != null)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + payments.UploadedFile.FileName;
                    filePath = Path.Combine(uploadsFolder, uniqueName);
                    payments.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    payments.ImageName = uniqueName;
                }
            }
            catch
            {

            }
            payments.PaidOn = DateTime.UtcNow;
            if (property.PropertyModeId == 1)
            {
                payments.ExpiryDate = payments.PaidOn.AddMonths(1);
            }
            else
            {
                payments.ExpiryDate = DateTime.UtcNow.AddYears(70);
            }
            payments.RecieptNumber = new Guid().ToString();
            _context.Payments.Add(payments);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecieptNumber,PaidOn,ExpiryDate,ImageName,PropertyId,TenantId,LandLordId")] Payments payments)
        {
            ViewBag.NavigatedTO = "Payment";
            if (ModelState.IsValid)
            {
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LandLordId"] = new SelectList(_context.Users, "Id", "Id", payments.LandLordId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", payments.PropertyId);
            ViewData["TenantId"] = new SelectList(_context.Users, "Id", "Id", payments.TenantId);
            return View(payments);
        }


        [Authorize(Roles = "Admin, Agent")]
        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Payment";
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            ViewData["LandLordId"] = new SelectList(_context.Users, "Id", "Id", payments.LandLordId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", payments.PropertyId);
            ViewData["TenantId"] = new SelectList(_context.Users, "Id", "Id", payments.TenantId);
            return View(payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecieptNumber,PaidOn,ExpiryDate,ImageName,PropertyId,TenantId,LandLordId")] Payments payments)
        {
            ViewBag.NavigatedTO = "Payment";
            if (id != payments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsExists(payments.Id))
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
            ViewData["LandLordId"] = new SelectList(_context.Users, "Id", "Id", payments.LandLordId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Address", payments.PropertyId);
            ViewData["TenantId"] = new SelectList(_context.Users, "Id", "Id", payments.TenantId);
            return View(payments);
        }


        [Authorize(Roles = "Admin")]
        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Payment";
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Include(p => p.LandLord)
                .Include(p => p.Property)
                .Include(p => p.Tenant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Payment";
            var payments = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsExists(int id)
        {
            ViewBag.NavigatedTO = "Payment";
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
