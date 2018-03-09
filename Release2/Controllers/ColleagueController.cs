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
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ColleagueType = user.ColleagueType,
                    Department = user.Department.DepartmentName,
                    EmploymentType = user.EmploymentType,
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
                };

                var result = UserManager.Create(colleague, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Colleague/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Colleague/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Colleague/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Colleague/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
