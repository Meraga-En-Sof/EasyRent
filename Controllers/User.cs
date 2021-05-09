using EasyRent.Constants;
using EasyRent.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyRent.Controllers
{
  
        [Authorize]
        public class User : Controller
        {
            private readonly ApplicationDbContext db;

            public User(ApplicationDbContext context)
            {
                db = context;

            }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
            {
                ViewBag.NavigatedTO = "Users";

                var users = db.Users.Where(m => m.Email != "Support01@EasyRent.com");
                return View(users);
            }


        public IActionResult Landlords()
        {
            ViewBag.NavigatedTO = "Landlord";

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<EasyRent.Models.User> applicationUsers = new List<EasyRent.Models.User>();
                var roles = db.Properties.Where(m => m.ClosedToId == userId);

                foreach (var item in roles)
                {
                    var user = db.Users.Where(m => m.Id == item.UserId).FirstOrDefault();
                    applicationUsers.Add(user);
                }




                var users = applicationUsers;
                return View(users);
            }

            var userroles = db.UserRoles.Where(m => m.RoleId == "002");

            List<EasyRent.Models.User> landlords = new List<Models.User>();

            foreach (var item in userroles)
            {
                var user = db.Users.Where(m => m.Id == item.UserId).FirstOrDefault();
                landlords.Add(user);
            }
            var all_landlords = landlords;
            return View(all_landlords);
        }

        public IActionResult Tenants()
        {
            ViewBag.NavigatedTO = "Tenants";

            if (!User.IsInRole("Admin"))
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<EasyRent.Models.User> applicationUsers = new List<EasyRent.Models.User>();
                var roles = db.Properties.Where(m => m.UserId == userId);

                foreach (var item in roles)
                {
                    var user = db.Users.Where(m => m.Id == item.ClosedToId).FirstOrDefault();
                    applicationUsers.Add(user);
                }




                var users = applicationUsers;
                return View(users);
            }

            var userroles = db.UserRoles.Where(m => m.RoleId == "003");

            List<EasyRent.Models.User> tenants = new List<Models.User>();

            foreach (var item in userroles)
            {
                var user = db.Users.Where(m => m.Id == item.UserId).FirstOrDefault();
                tenants.Add(user);
            }
            var all_tenants = tenants;
            return View(all_tenants);
        }


      //Actions


            public IActionResult Delete(string Id)
            {



                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {

                    }
                    else if (role.RoleId == "000")
                    {
                        if (User.IsInRole("Admin"))
                        {

                            db.UserRoles.Remove(role);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        db.UserRoles.Remove(role);
                        db.SaveChanges();
                    }

                }
                catch
                {

                }
                ViewBag.NavigatedTO = "Users";
                var users = db.Users;
                return RedirectToAction("Index", users);
            }

            public IActionResult Tenant(string Id)
            {

                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {
                    ViewBag.NavigatedTO = "Tenants";
                    var usersd = db.Users;
                    return RedirectToAction("Index", usersd);
                }
                    else if (role.RoleId == GetUserRoles.Tenant)
                    {
                       
                    }
                    else
                    {
                            IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>()
                            {
                                UserId = role.UserId,
                                RoleId = GetUserRoles.Tenant
                            };


                        
                                db.UserRoles.Remove(role);
                                db.SaveChanges();
                            db.UserRoles.Add(identityUserRole);
                            db.SaveChanges();
                    }

                }
                catch
                {
                   
                }
                ViewBag.NavigatedTO = "Users";
                var users = db.Users;
                return RedirectToAction("Index", users);
            }


            public IActionResult Agent(string Id)
            {

                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {
                        ViewBag.NavigatedTO = "Landlords";
                        var usersdd = db.Users;
                        return RedirectToAction("Index", usersdd);
                    }
                    else if (role.RoleId == GetUserRoles.Admin)
                    {
                       
                    }
                    else
                    {
                            IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>()
                            {
                                UserId = role.UserId,
                                RoleId = GetUserRoles.Agent
                            };



                            db.UserRoles.Remove(role);
                            db.SaveChanges();
                            db.UserRoles.Add(identityUserRole);
                            db.SaveChanges();
                    }


                }
                catch
                {
                    
                }
                ViewBag.NavigatedTO = "Users";
                var users = db.Users;
                return RedirectToAction("Index", users);
            }


            public class UserRoles
            {
                public string UserId { get; set; }
                public string RoleId { get; set; }
            }


        }
    }
