using EasyRent.Data;
using EasyRent.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Controllers
{

    [Authorize]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext db;

        public Dashboard(ApplicationDbContext context)
        {
            db = context;

        }
        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Profile");
            }
            ViewBag.NavigatedTO = "home";

            var users = db.Users;
            var products = db.Properties.Include(m => m.PropertyMode).Include(m => m.PropertyType);
            var maxPrice = products.OrderBy(m => m.Price).Select(m => m.Price).FirstOrDefault();
            var lowestpreice = products.OrderByDescending(m => m.Price).Select(m => m.Price).FirstOrDefault();
            double currentprice = 0;
            var totalproperties = products.Count();
            foreach (var item in products)
            {
                currentprice += item.Price;

            }
            var averageprice = currentprice / totalproperties;
            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                HigestPrice = maxPrice,
                LowestPrice = lowestpreice,
                AveragePrice = averageprice,
                MemberNumber = users.Count(),
                Properties = products,
                MessageNumbers = db.Messages.Count(),
                PropertyNumber = totalproperties,

                Users = users,
                PurchaseNumber = products.Where(m => m.PropertyMode.Name != "Rent").Count(),
                RentNumber = products.Where(m => m.PropertyMode.Name == "Rent").Count(),


            };
            return View(dashboardViewModel);
        }


    }


}

