using EasyRent.Data;
using EasyRent.Models;
using EasyRent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {

            UserHomeViewModel userGeneralViewModel = new UserHomeViewModel()
            {
                SliderProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true),
                OurServices = db.OurServices.OrderByDescending(m => m.Id),
              RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Where(m => m.isDealClosed == false),
              PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed),
                PropertyModes = db.propertyModes,
                PropertyTypes = db.PropertyTypes,
                Testimonials = db.Testimonials,
                AllProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
           
            return View(userGeneralViewModel);
        }

        public IActionResult About()
        {
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel()
            {   AboutUs = db.AboutUs.OrderByDescending(m => m.Id).FirstOrDefault(),
                Testimonials = db.Testimonials,
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            var roles = db.UserRoles.Where(m => m.RoleId == "002").Select(m => m.UserId);

            List<User> applicationUsers = new List<User>();

            foreach (var item in roles)
            {
                var user = db.Users.Where(m => m.Id == item).FirstOrDefault();
                applicationUsers.Add(user);
            }
            aboutUsViewModel.Agents = applicationUsers;
            return View(aboutUsViewModel);
        }

        public IActionResult Faq()
        {
            UserFaqViewModel userFaqViewModel = new UserFaqViewModel()
            {
                Faqs = db.Fag,
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            return View(userFaqViewModel);
        }


        public IActionResult PropertyDetail(int Id)
        {
            return View();
        }

        public IActionResult AgentDetail(int Id)
        {
            return View();
        }


        public IActionResult Gallery()
        {
            UserGalleryViewModel userGalleryViewModel = new UserGalleryViewModel()
            {
                Properties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                PropertyTypes = db.PropertyTypes,
                MenuProperties = db.Properties.OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            return View(userGalleryViewModel);
        }

        public IActionResult ContactUs()
        {
            UserContactUsViewModel userContactUsViewModel = new UserContactUsViewModel()
            {
                ContactInformation = db.ContactInformation.OrderByDescending(m => m.Id).FirstOrDefault(),
                
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            return View(userContactUsViewModel);
        }

        [HttpPost]
        public IActionResult ContactUs(UserContactUsViewModel userContactUsViewModel)
        {

            userContactUsViewModel.MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10);
            userContactUsViewModel.SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {

            }
            return View(userContactUsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
