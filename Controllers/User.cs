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
                var users = db.Users;
                return View(users);
            }


        public IActionResult Landlords()
        {
            ViewBag.NavigatedTO = "Landlord";
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<EasyRent.Models.User> applicationUsers = new List<EasyRent.Models.User>();
            var roles = db.Properties.Where(m=> m.UserId == userId);

            foreach (var item in roles)
            {
                var user = db.Users.Where(m => m.Id == item.ClosedToId).FirstOrDefault();
                applicationUsers.Add(user);
            }



            
            var users = applicationUsers;
            return View(users);
        }

        public IActionResult Tenants()
        {
            ViewBag.NavigatedTO = "Tenants";


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


        public IActionResult Team(string Id)
            {



                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {

                    }
                    else if (role.RoleId == GetUserRoles.Team)
                    {
                        if (User.IsInRole("Admin"))
                        {
                            role.RoleId = GetUserRoles.Team;
                            db.UserRoles.Update(role);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        role.RoleId = GetUserRoles.Team;
                        db.UserRoles.Update(role);
                        db.SaveChanges();
                    }

                }
                catch
                {
                    IdentityUserRole<string> userRoles = new IdentityUserRole<string>()
                    {
                        UserId = Id,
                        RoleId = GetUserRoles.Team
                    };



                    db.UserRoles.Add(userRoles);
                    db.SaveChanges();
                }
                ViewBag.NavigatedTO = "Users";
                var users = db.Users;
                return RedirectToAction("Index", users);
            }


            public IActionResult Delete(string Id)
            {



                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {

                    }
                    else if (role.RoleId == GetUserRoles.Team)
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

            public IActionResult Blog(string Id)
            {

                var role = db.UserRoles.Where(m => m.UserId == Id).FirstOrDefault();
                try
                {
                    if (role.RoleId == GetUserRoles.Admin)
                    {

                    }
                    else if (role.RoleId == GetUserRoles.Team)
                    {
                        if (User.IsInRole("Admin"))
                        {
                            role.RoleId = GetUserRoles.Blog;
                            db.UserRoles.Update(role);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        role.RoleId = GetUserRoles.Blog;
                        db.UserRoles.Update(role);
                        db.SaveChanges();
                    }

                }
                catch
                {
                    IdentityUserRole<string> userRoles = new IdentityUserRole<string>()
                    {
                        UserId = Id,
                        RoleId = GetUserRoles.Blog
                    };



                    db.UserRoles.Add(userRoles);
                    db.SaveChanges();
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

                    }
                    else if (role.RoleId == GetUserRoles.Team)
                    {
                        if (User.IsInRole("Admin"))
                        {
                            role.RoleId = GetUserRoles.Agent;
                            db.UserRoles.Update(role);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        role.RoleId = GetUserRoles.Agent;
                        db.UserRoles.Update(role);
                        db.SaveChanges();
                    }


                }
                catch
                {
                    IdentityUserRole<string> userRoles = new IdentityUserRole<string>()
                    {
                        UserId = Id,
                        RoleId = GetUserRoles.Agent
                    };



                    db.UserRoles.Add(userRoles);
                    db.SaveChanges();
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
