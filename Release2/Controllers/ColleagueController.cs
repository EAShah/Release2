using Microsoft.AspNet.Identity;
using Project._1.Models;
using Release2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Release2.Models;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;

namespace Release2.Controllers
{    
    public class ColleagueController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ColleagueController()
        {
        }

        public ColleagueController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //public object UserManeger { get; private set; }

        /// <summary>
        /// This action lists colleague users
        /// </summary>
        // GET: Colleague
        public ActionResult Index()
        {
            var users = db.Colleagues.ToList();
            var model = new List<ColleagueViewModel>();
            foreach (var user in users)
            {
                if (!(user is ProbationaryColleague))
                {
                    model.Add(new ColleagueViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        
                        ColleagueType = user.ColleagueType,
                        EmploymentType = user.EmploymentType,
                        Department = user.Department.DepartmentName,
                        ColleagueRegion = user.ColleagueRegion
                    });
                }
            }
            return View(model);
        }

        /// <summary>
        /// This action lists colleague details
        /// </summary>
        // GET: Colleague/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                // Convert id to int instead of int?
                int userId = id ?? default(int);

                // find the user in the database
                var user = UserManager.FindById(userId);

                // Check if the user exists
                if (user != null)
                {
                    var colleague = (Colleague)user;

                    // Use Automapper instead of copying properties one by one
                    ColleagueViewModel model = Mapper.Map<ColleagueViewModel>(colleague);

                    model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());

                    return View(model);
                }

                else
                {
                    // Customize the error view: /Views/Shared/Error.cshtml
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// This action creates colleague users
        /// </summary>
        // GET: Colleague/Create
        public ActionResult Create()
        {
            //NOTE: Add department list
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Colleague/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColleagueViewModel model, params string[] roles)
        {
            if (ModelState.IsValid)
            {
                var colleague = new Colleague
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ColleagueType = model.ColleagueType,
                    DepartmentId = model.DepartmentId,
                    EmploymentType = model.EmploymentType,
                    ColleagueRegion = model.ColleagueRegion,
                };

                var result = UserManager.Create(colleague, model.Password);

                if (result.Succeeded)
                {
                    // Add user to selected roles
                    var roleResult = UserManager.AddToRoles(colleague.Id, roles);

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Display error messages in the view @Html.ValidationSummary()
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());

                        // Create a check list object
                        ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");

                        // Return a view if you want to see error message saved in ModelState
                        // Redirect() and RedirectToAction() clear the messages
                        return View();
                    }
                }
                else
                {
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
                    return View();
                }
            }

            else
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
                return View();
            }
        }

        /// <summary>
        /// This action edits colleague users
        /// </summary>
        // GET: Colleague/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);

                var colleague = (Colleague)UserManager.FindById(userId);
                if (colleague == null)
                {
                    return View("Error");
                }

                // Use automapper instead of copying properties one by one
                ColleagueViewModel model = Mapper.Map<ColleagueViewModel>(colleague);

                var userRoles = UserManager.GetRoles(userId);
                var rolesSelectList = db.Roles.ToList().Select(r => new SelectListItem()
                {
                    Selected = userRoles.Contains(r.Name),
                    Text = r.Name,
                    Value = r.Name
                });

                ViewBag.RolesSelectList = rolesSelectList;

                // Prepare the dropdown list
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", colleague.DepartmentId);
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Colleague/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ColleagueViewModel model, params string[] roles)
        {
            
                ModelState.Remove("Password");
                ModelState.Remove("Confirm Password");

                if (ModelState.IsValid)
                {
                    var colleague = (Colleague)UserManager.FindById(id);
                    if (colleague == null)
                    {
                        return HttpNotFound();
                    }

                    // Edit colleague information
                    colleague.UserName = model.UserName;
                    colleague.Email = model.Email;
                    colleague.FirstName = model.FirstName;
                    colleague.LastName = model.LastName;
                    colleague.ColleagueType = model.ColleagueType;
                    colleague.DepartmentId = model.DepartmentId;
                    colleague.EmploymentType = model.EmploymentType;
                    colleague.ColleagueRegion = model.ColleagueRegion;

                    var userResult = UserManager.Update(colleague);

                if (userResult.Succeeded)
                {
                    var userRoles = UserManager.GetRoles(colleague.Id);
                    roles = roles ?? new string[] { };
                    var roleResult = UserManager.AddToRoles(colleague.Id, roles.Except(userRoles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    roleResult = UserManager.RemoveFromRoles(colleague.Id, userRoles.Except(roles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    return RedirectToAction("Index");
                }
                }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        /// <summary>
        /// This action deletes colleague users
        /// </summary>
        // GET: Colleague/Delete/5
        public ActionResult Delete(int id)
        {
            var colleague = (Colleague)UserManager.FindById(id);
            if (colleague == null)
            {
                return HttpNotFound();
            }

            ColleagueViewModel model = new ColleagueViewModel
            {
                Id = colleague.Id,
                Email = colleague.Email,
                FirstName = colleague.FirstName,
                LastName = colleague.LastName,
                ColleagueType = colleague.ColleagueType,
                Department = colleague.Department.DepartmentName,
                EmploymentType = colleague.EmploymentType,
                ColleagueRegion = colleague.ColleagueRegion,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            return View(model);
        }

        // POST: Colleague/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //try
            //{
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");

                if (ModelState.IsValid)
                {
                    var user = UserManager.FindById(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }

                    var result = UserManager.Delete(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                
                return RedirectToAction("Index");
        }
    }
}
