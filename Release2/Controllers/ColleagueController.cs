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

        public object UserManeger { get; private set; }


        // GET: Colleague
        public ActionResult Index()
        {
            var users = db.Colleagues.ToList();
            var model = new List<ColleagueViewModel>();
            foreach (var user in users)
            {
                model.Add(new ColleagueViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ColleagueType = user.ColleagueType,
                    EmploymentType = user.EmploymentType,
                    Department = user.Department.DepartmentName,
                    ColleagueRegion = user.ColleagueRegion
                });
            }
            return View(model);
        }

        // GET: Colleague/Details/5
        public ActionResult Details(int id)
        {
            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var colleague = (Colleague)user;

                ColleagueViewModel model = new ColleagueViewModel()
                {
                    UserName = colleague.UserName,
                    Email = colleague.Email,
                    FirstName = colleague.FirstName,
                    LastName = colleague.LastName,
                    ColleagueType = colleague.ColleagueType,
                    Department = colleague.Department.DepartmentName,
                    EmploymentType = colleague.EmploymentType,
                    ColleagueRegion = colleague.ColleagueRegion
                };

                return View(model);
            }
            else
            {
                // Customize the error view: /Views/Shared/Error.cshtml
                return View("Error");
            }
        }

        // GET: Colleague/Create
        public ActionResult Create()
        {
            //NOTE: Add department list
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Colleague/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColleagueViewModel model)
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
                    ColleagueRegion = model.ColleagueRegion
                };

                var result = UserManager.Create(colleague, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                    return View();
                }
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // GET: Colleague/Edit/5
        public ActionResult Edit(int id)
        {
            var colleague = (Colleague)UserManager.FindById(id);
            if (colleague == null)
            {
                return View("Error");
            }

            ColleagueViewModel model = new ColleagueViewModel
            {
                Id = colleague.Id,
                Email = colleague.Email,
                FirstName = colleague.FirstName,
                LastName = colleague.LastName,
                ColleagueType = colleague.ColleagueType,
                DepartmentId = colleague.DepartmentId,
                EmploymentType = colleague.EmploymentType,
                ColleagueRegion = colleague.ColleagueRegion
            };

            // Prepare the dropdown list
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", colleague.DepartmentId);
            return View(model);
        }

        // POST: Colleague/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ColleagueViewModel model)
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
                        return RedirectToAction("Index");
                    }
                }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

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
                ColleagueRegion = colleague.ColleagueRegion
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
