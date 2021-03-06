using EasyRent.Data;
using EasyRent.Models;
using EasyRent.ViewModels;
using KigooProperties.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
                SliderProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDisplayed == true),
                OurServices = db.OurServices.OrderByDescending(m => m.Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed),
                PropertyModes = db.propertyModes,
                PropertyTypes = db.PropertyTypes,
                Testimonials = db.Testimonials.Include(m => m.User).Where(m => m.isApproved == true),
                AllProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
           
            return View(userGeneralViewModel);
        }

       
        public IActionResult LargeFilter(UserHomeViewModel zeuserHomeViewModel)
        {
            QueryGenerally queryGenerally = new QueryGenerally(db);
            GeneralQuery generalQuery = new GeneralQuery()
            {
                Country = zeuserHomeViewModel.Country,
                Keyword = zeuserHomeViewModel.Keyword,
                MaxPrice = zeuserHomeViewModel.MaxPrice,
                MinPrice = zeuserHomeViewModel.MinPrice,
                NumberOfBathrooms = zeuserHomeViewModel.NumberOfBathrooms,
                NumberofBedrooms = zeuserHomeViewModel.NumberofBedrooms,
                PropertyStatus = zeuserHomeViewModel.PropertyStatus,
                PropertyType = zeuserHomeViewModel.PropertyType
            };

            

            UserHomeViewModel userGeneralViewModel = new UserHomeViewModel()
            {
                SliderProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true),
                OurServices = db.OurServices.OrderByDescending(m => m.Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id),
                PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed),
                PropertyModes = db.propertyModes,
                PropertyTypes = db.PropertyTypes,
                Testimonials = db.Testimonials.Include(m => m.User).Where(m => m.isApproved == true),
                AllProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };
            userGeneralViewModel.RecentProperties = queryGenerally.GetPropertieszz(generalQuery);
            return View("PropertyList", userGeneralViewModel);
        }
        public IActionResult PropertyList()
        {

            UserHomeViewModel userGeneralViewModel = new UserHomeViewModel()
            {
                SliderProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true),
                OurServices = db.OurServices.OrderByDescending(m => m.Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id),
                PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed),
                PropertyModes = db.propertyModes,
                PropertyTypes = db.PropertyTypes,
                Testimonials = db.Testimonials.Include(m => m.User).Where(m => m.isApproved == true),
                AllProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };

            return View(userGeneralViewModel);
        }

        public IActionResult PropertyFilter(string Id)
        {

            UserHomeViewModel userGeneralViewModel = new UserHomeViewModel()
            {
                SliderProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true),
                OurServices = db.OurServices.OrderByDescending(m => m.Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Where(m => m.isDealClosed == false),
                PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed),
                PropertyModes = db.propertyModes,
                PropertyTypes = db.PropertyTypes,
                Testimonials = db.Testimonials.Include(m => m.User).Where(m => m.isApproved == true),
                AllProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()
            };

            if (!string.IsNullOrEmpty(Id))
            {
                userGeneralViewModel.RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).OrderByDescending(m => m.Id).Where(m =>  m.Address.Contains(Id));
            }

            return View("PropertyList", userGeneralViewModel);
        }




        public IActionResult About()
        {
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel()
            {   AboutUs = db.AboutUs.OrderByDescending(m => m.Id).FirstOrDefault(),
                Testimonials = db.Testimonials.Include(m => m.User).Where(m => m.isApproved == true),
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
                PropertyAmenities = db.PropertyAmenities.Include(m => m.Amenities).Include(m => m.Property).Where(m => m.PropertyId == Id),
                RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).Where(m => m.isDisplayed==true).OrderByDescending(m => m.Id).Take(3),
                PropertySliders = db.PropertySliders.Where(m => m.PropertiesId == Id),
                MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).OrderByDescending(m => m.Id).Take(10),
                SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault()

            };

            userPropertyDetailViewModel.User = userPropertyDetailViewModel.Property.User;
            return View(userPropertyDetailViewModel);
        }

        [HttpPost, Authorize]
        public IActionResult PropertyDetail(UserPropertyDetailViewModel userPropertyDetailViewModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int Id = userPropertyDetailViewModel.Property.Id;

            userPropertyDetailViewModel.PopularProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.isDealClosed == true && m.isDisplayed == true);
            userPropertyDetailViewModel.Property = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Where(m => m.Id == Id).Include(m => m.User).FirstOrDefault();
            userPropertyDetailViewModel.PropertyAmenities = db.PropertyAmenities.Include(m => m.Amenities).Include(m => m.Property).Where(m => m.PropertyId == Id);
            userPropertyDetailViewModel.RecentProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User)
                .Where(m => m.isDisplayed == true).OrderByDescending(m => m.Id).Take(3);
            userPropertyDetailViewModel.PropertySliders = db.PropertySliders.Where(m => m.PropertiesId == Id);
            userPropertyDetailViewModel.MenuProperties = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType).Include(m => m.User).OrderByDescending(m => m.Id).Take(10);
            userPropertyDetailViewModel.SocialMedia = db.SocialMedias.OrderByDescending(m => m.Id).FirstOrDefault();

            try
            { 
                Messages messages = new Messages()
                {
                    DateSent = DateTime.UtcNow,
                    Content = userPropertyDetailViewModel.MessageForm,
                    SenderId = userId,
                    RecieverId = userPropertyDetailViewModel.User.Id
                };
                db.Messages.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index","Messages");
            }
            catch
            {

            }

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
                db.ContactUs.Add(userContactUsViewModel.ContactUsForm);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
