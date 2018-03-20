using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project._1.Models;
using Release2.Models;
using Release2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Release2.Controllers
{
    public class ProbationaryColleagueController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ProbationaryColleagueController()
        {
        }

        public ProbationaryColleagueController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: ProbationaryColleague
        public ActionResult Index()
        {
            var users = db.ProbationaryColleagues.ToList();
            var model = new List<ProbationaryColleagueViewModel>();
            foreach (var user in users)
            {
                model.Add(new ProbationaryColleagueViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Department = user.Department.DepartmentName,
                    ColleagueRegion = user.ColleagueRegion,
                    Level = user.Level,
                    CityOfProbation = user.CityOfProbation,
                    ProbationType = user.ProbationType,
                    JoinDate = user.JoinDate
                });
            }
            return View(model);
        }

        // GET: ProbationaryColleague/Details/5
        public ActionResult Details(int id)
        {

            // find the user in the database
            var user = UserManager.FindById(id);

            // Check if the user exists
            if (user != null)
            {
                var probationaryColleague = (ProbationaryColleague)user;

                ProbationaryColleagueViewModel model = new ProbationaryColleagueViewModel()
                {
                    Id = probationaryColleague.Id,
                    Email = probationaryColleague.Email,
                    FirstName = probationaryColleague.FirstName,
                    LastName = probationaryColleague.LastName,
                    Level = probationaryColleague.Level,
                    Department = probationaryColleague.Department.DepartmentName,
                    ColleagueRegion = probationaryColleague.ColleagueRegion,
                    CityOfProbation = probationaryColleague.CityOfProbation,
                    ProbationType = probationaryColleague.ProbationType,
                    JoinDate = probationaryColleague.JoinDate,
                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                return View(model);
            }
            else
            {
                // Customize the error view: /Views/Shared/Error.cshtml
                return View("Error");
            }
        }

        // GET: ProbationaryColleague/Create
        public ActionResult Create()
        {
            // Fill in the ViewBag for the dropdown list
            // Id is the value of the HTML select
            // Name is the Text of the HTML selct
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: ProbationaryColleague/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProbationaryColleagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find department
                var probationaryColleague = new ProbationaryColleague
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ColleagueRegion = model.ColleagueRegion,
                    CityOfProbation = model.CityOfProbation,
                    ProbationType = model.ProbationType,
                    JoinDate = model.JoinDate,
                    Level = model.Level,
                    DepartmentId = model.DepartmentId,
                };

                var result = UserManager.Create(probationaryColleague, model.Password);

                if (result.Succeeded)
                {
                    //TODO Add user to ProbationaryColleague role (check if ProbationaryColleague role exists)
                    var roleResult = UserManager.AddToRoles(probationaryColleague.Id, "Probationary Colleague");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                        // Display error messages in the view @Html.ValidationSummary()
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }
                }
                else
                {
                    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                    // Display error messages in the view @Html.ValidationSummary()
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    return View();
                }
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();

        }

        // GET: ProbationaryColleague/Edit/5
        public ActionResult Edit(int id)
        {

            var probationaryColleague = (ProbationaryColleague)UserManager.FindById(id);

            if (probationaryColleague == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            ProbationaryColleagueViewModel model = new ProbationaryColleagueViewModel
            {
                Id = probationaryColleague.Id,
                Email = probationaryColleague.Email,
                FirstName = probationaryColleague.FirstName,
                LastName = probationaryColleague.LastName,
                ColleagueRegion = probationaryColleague.ColleagueRegion,
                CityOfProbation = probationaryColleague.CityOfProbation,
                ProbationType = probationaryColleague.ProbationType,
                JoinDate = probationaryColleague.JoinDate,
                Level = probationaryColleague.Level,
                DepartmentId = probationaryColleague.DepartmentId,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };

            // Prepare the dropdown list
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", probationaryColleague.DepartmentId);
            return View(model);
        }

        // POST: ProbationaryColleague/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProbationaryColleagueViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var probationaryColleague = (ProbationaryColleague)UserManager.FindById(id);
                if (probationaryColleague == null)
                {
                    return HttpNotFound();
                }

                // Edit the ProbationaryColleague info
                probationaryColleague.Email = model.Email;
                probationaryColleague.FirstName = model.FirstName;
                probationaryColleague.LastName = model.LastName;
                probationaryColleague.UserName = model.Email;
                probationaryColleague.ColleagueRegion = model.ColleagueRegion;
                probationaryColleague.CityOfProbation = model.CityOfProbation;
                probationaryColleague.ProbationType = model.ProbationType;
                probationaryColleague.JoinDate = model.JoinDate;
                probationaryColleague.Level = model.Level;
                probationaryColleague.DepartmentId = model.DepartmentId;

                var userResult = UserManager.Update(probationaryColleague);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", model.DepartmentId);
            return View();
        }

        // GET: ProbationaryColleague/Delete/5
        public ActionResult Delete(int id)
        {
            var probationaryColleague = (ProbationaryColleague)UserManager.FindById(id);
            if (probationaryColleague == null)
            {
                return HttpNotFound();
            }

            ProbationaryColleagueViewModel model = new ProbationaryColleagueViewModel
            {
                Id = probationaryColleague.Id,
                Department = probationaryColleague.Department.DepartmentName,
                Email = probationaryColleague.Email,
                FirstName = probationaryColleague.FirstName,
                LastName = probationaryColleague.LastName,
                Level = probationaryColleague.Level,
                ColleagueRegion = probationaryColleague.ColleagueRegion,
                CityOfProbation = probationaryColleague.CityOfProbation,
                ProbationType = probationaryColleague.ProbationType,
                JoinDate = probationaryColleague.JoinDate,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);
        }

        // POST: ProbationaryColleague/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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

            return View();
        }
    }
}
