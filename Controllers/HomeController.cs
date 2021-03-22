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

            List<EasyRent.Models.User> applicationUsers = new List<EasyRent.Models.User>();

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
            UserPropertyDetailViewModel userPropertyDetailViewModel = new UserPropertyDetailViewModel()
            {
                PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true),
                Property = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.Id== Id).Include(m => m.User).FirstOrDefault(),
                PropertyAmenities = db.PropertyAmenities.Include(m => m.Amenities).Include(m => m.Property).Where(m => m.Id == Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).Where(m => m.isDisplayed==true).OrderByDescending(m => m.Id).Take(3),
                PropertySliders = db.PropertySliders.Where(m => m.PropertiesId == Id),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()

            };

            userPropertyDetailViewModel.User = userPropertyDetailViewModel.Property.User;
            return View(userPropertyDetailViewModel);
        }

        public IActionResult AgentDetail(string Id)
        {

            AgentDetailViewModel agentDetailViewModel = new AgentDetailViewModel()
            {
                Agent = db.Users.Where(m => m.Id == Id).FirstOrDefault(),
                Properties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).Where(m => m.UserId == Id),
                PropertyTypes = db.PropertyTypes,
                Recent = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).OrderByDescending(m => m.Id).Take(3),
                
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            return View(agentDetailViewModel);
        }


        public IActionResult Gallery()
        {
            UserGalleryViewModel userGalleryViewModel = new UserGalleryViewModel()
            {
                Properties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                PropertyTypes = db.PropertyTypes,
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
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
