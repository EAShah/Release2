using Microsoft.AspNet.Identity;
using Project._1.Models;
using Release2.Models;
using Release2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Release2.Controllers
{
    public class AssignmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists assignments for Line managers to HR Associates
        /// </summary>
        // GET: Assignment
        //[Authorize(Roles ="HR Associate")]
        public ActionResult HRIndex()
        {
            var assignment = db.Assignments.ToList();
            var model = new List<AssignmentViewModel>();
            foreach (var item in assignment)
            {
                model.Add(new AssignmentViewModel
                {
                    Id = item.AssignmentId,
                    ProbationaryColleague = item.ProbationaryColleague.FullName,
                    LMAssigned = item.LMAssigned.FullName,
                    AssignmentDate = item.AssignmentDate,
                    AssignmentStatus = item.AssignmentStatus,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action lists assignments for Line managers to Line managers
        /// </summary>
        // GET: Assignment
        //[Authorize(Roles = "Line Manager")]
        public ActionResult LMIndex()
        {
            var assignment = db.Assignments.ToList();
            var model = new List<AssignmentViewModel>();
            foreach (var item in assignment)
            {
                if (item.LMAssignID == User.Identity.GetUserId<int>())
                {
                    model.Add(new AssignmentViewModel
                    {
                        Id = item.AssignmentId,
                        ProbationaryColleague = item.ProbationaryColleague.FullName,
                        LMAssigned = item.LMAssigned.FullName,
                        AssignmentDate = item.AssignmentDate,
                        AssignmentStatus = item.AssignmentStatus,
                        ProbationType = item.ProbationaryColleague.ProbationType,
                        Department = item.ProbationaryColleague.Department.DepartmentName,
                        HRAssigns = item.HRAssigns.FullName
                    });
                }
                
            }
            return View(model);
        }

        /// <summary>
        /// This action lists assignment details for Line managers
        /// </summary>
        //[Authorize(Roles ="HR Associate, Line Manager")]
        // GET: Assignment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        /// <summary>
        /// This action creates assignments for Line managers
        /// </summary>
        // GET: Assignment/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.PCID = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FullName");
            ViewBag.LMAssignedID = new SelectList(db.Colleagues, "ColleagueId", "FullName");
            return View();
        }

        // POST: Assignment/Create
        [HttpPost]
        public ActionResult Create(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var assignment = new Assignment
                {
                    PCID = model.PCID,
                    LMAssignID = model.LMAssignID,
                    HRAssignID = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    AssignmentDate = DateTime.Now,
                    AssignmentStatus= Assignment.AssignStatus.Pending,
                };

                db.Assignments.Add(assignment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.PCID = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FullName");
                ViewBag.LMAssignedID = new SelectList(db.Colleagues, "ColleagueId", "FullName");
                return View(model);
            }
        }

        /// <summary>
        /// This action inspects assignments for Line managers
        /// </summary>
        // GET: Assignment/Inspect/5
        public ActionResult Inspect(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            AssignmentViewModel model = new AssignmentViewModel
            {
                Id = assignment.AssignmentId,
                ProbationaryColleague = assignment.ProbationaryColleague.FullName,
                LMAssigned = assignment.LMAssigned.FullName,
                AssignmentDate = assignment.AssignmentDate,
                AssignmentStatus = assignment.AssignmentStatus,
                HRAssigns = assignment.HRAssigns.FullName,
                AssignmentInspectionDate= DateTime.Now
            };

            return View(model);
        }

        // POST: Assignment/Inspect/5
        [HttpPost]
        public ActionResult Inspect(int id, AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var assignment = db.Assignments.Find(id);
                if (assignment != null)
                {
                    assignment.LMAssignID = model.LMAssignID;
                    assignment.AssignmentDate = model.AssignmentDate;
                    assignment.AssignmentStatus = model.AssignmentStatus;
                    assignment.PCID = model.PCID;
                    assignment.HRAssignID = model.HRAssignID;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
