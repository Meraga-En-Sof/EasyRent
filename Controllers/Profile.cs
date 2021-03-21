using EasyRent.Constants;
using EasyRent.Data;
using EasyRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyRent.Controllers
{
    [Authorize]
    public class Profile : Controller
    {
        private readonly ApplicationDbContext db;
        private IHostingEnvironment _env;

        public Profile(IHostingEnvironment env, ApplicationDbContext context)
        {
            db = context;
            _env = env;
        }
        public IActionResult Index(string Id)
        {
            ViewBag.IsUser = false;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (String.IsNullOrEmpty(Id))
            {
                ViewBag.IsUser = true;
                Id = userId;
            }

            if (Id == userId)
            {
                ViewBag.IsUser = true;
            }
            ViewBag.NavigatedTO = "Profile";


            var profile = db.Users.Where(m => m.Id == Id).FirstOrDefault();


            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User applicationUser)
        {
            ViewBag.NavigatedTO = "Profile";
            var user = db.Users.Where(m => m.Id == applicationUser.Id).FirstOrDefault();
            user.LastName = applicationUser.LastName;
            user.FirstName = applicationUser.FirstName;
            user.MiddleName = applicationUser.MiddleName;
            user.About = applicationUser.About;
            user.Address = applicationUser.Address;
            user.FacebookLink = applicationUser.FacebookLink;
            user.TwitterLink = applicationUser.TwitterLink;
            user.MobileNumber = applicationUser.MobileNumber;
            user.Quote = applicationUser.Quote;
            user.InstagramLink = applicationUser.InstagramLink;
            user.LinkedInLink = applicationUser.LinkedInLink;
            user.Language = applicationUser.Language;
            user.Skills = applicationUser.Skills;

            if (String.IsNullOrEmpty(user.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {




                try
                {
                    var iscorrectformat = false;
                    string uniqueName = null;
                    string filePath = null;
                    FileInfo fi = new FileInfo(applicationUser.UploadedFile.FileName);

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
                        return View(applicationUser);
                    }

                    if (applicationUser.UploadedFile != null)
                    {
                        string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + applicationUser.UploadedFile.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueName);
                        applicationUser.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        applicationUser.ImageName = uniqueName;
                        user.ImageName = applicationUser.ImageName;
                    }
                }
                catch
                {

                }
                try
                {
                    db.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {

                }
            }

            return View("Index", applicationUser);
        }
    }



}
