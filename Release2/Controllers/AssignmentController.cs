using AutoMapper;
using Microsoft.AspNet.Identity;
using Project._1.Models;
using Release2.Models;
using Release2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// This action lists assignments for Line managers to HR Associates and Line managers
        /// </summary>
        /// <returns>Assignment, Index view</returns>
        // GET: Assignment
        [Authorize(Roles = "HRAssociate, LineManager")]
        public ActionResult Index()
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
        /// This action lists assignment details for Line managers
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Assignment, Details view</returns>
        [Authorize(Roles = "HRAssociate, LineManager")]
        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            var model = new AssignmentViewModel
            {
                Id = assignment.AssignmentId,
                ProbationaryColleague = assignment.ProbationaryColleague.FullName,
                LMAssigned = assignment.LMAssigned.FullName,
                Department = assignment.Department.DepartmentName,
                AssignmentDate = assignment.AssignmentDate,
                HRAssigns = assignment.HRAssigns.FullName,
                //AssignmentInspectionDate = assignment.AssignmentInspectionDate,
                AssignmentStatus = assignment.AssignmentStatus,

            };

            return View(model);
        }
        /// <summary>
        /// This action creates assignments for Line managers
        /// </summary>
        /// <param name="model", ></param>
        /// <returns>Assignment, Create view</returns>
        // GET: Assignment/Create
        [Authorize(Roles = "HRAssociate")]
        public ActionResult Create()
        {
            //var list = db.Colleagues.Where(t => t.Department == ProbationaryColleague.Department ).Select(c => new { c.Id, FullName = c.FirstName + " " + c.LastName });
            //var plist = db.Colleagues.Where(t => t = ColleagueType.ProbationaryColleague).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.PCId = new SelectList(db.ProbationaryColleagues.ToList(), "Id", "FullName");
            ViewBag.LMAssignId = new SelectList(db.Colleagues.ToList(), "Id", "FullName");
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
                    AssignmentId = model.Id,
                    PCId = model.PCId, 
                    DepartmentId = model.DepartmentId,
                    LMAssignId = model.LMAssignId,
                    HRAssignId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    //HRAssignId = model.HRAssignId,
                    AssignmentDate = DateTime.Today,
                    AssignmentStatus= Assignment.AssignStatus.Pending,
                };

                db.Assignments.Add(assignment);
                db.SaveChanges();
                //Utilities.SendEmail("eishah@dah.edu.sa", "pmscareem@gmail.com", "New Assignment - Probationary Colleague", "A probationary colleague has been assigned to you. Kindly visit the Probation Management site in order to review this assignment.");

                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.PCId = new SelectList(db.ProbationaryColleagues.ToList(), "Id", "FullName");
                ViewBag.LMAssignId = new SelectList(db.Colleagues.ToList(), "Id", "FullName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.PCId = new SelectList(db.ProbationaryColleagues.ToList(), "Id", "FullName");
                ViewBag.LMAssignId = new SelectList(db.Colleagues.ToList(), "Id", "FullName");
                return View(model);
            }
        }

        /// <summary>
        /// This action inspects assignments for Line managers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param> 
        /// <returns>Assignment, Inspect view</returns>
        // GET: Assignment/Inspect/5
        [Authorize(Roles = "LineManager")]
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

            //if (assignment.LMInspectId == assignment.LMAssignId)
            //{
            //AssignmentViewModel model = Mapper.Map<AssignmentViewModel>(assignment);
            AssignmentViewModel model = new AssignmentViewModel
            {
                Id = assignment.AssignmentId,
                LMInspectId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                PCId = assignment.PCId,
                ProbationaryColleague = assignment.ProbationaryColleague.FullName,
                LMAssignId = assignment.LMAssignId, // Include Ids in get as well as the properties. include hidden id in view
                LMAssigned = assignment.LMAssigned.FullName,
                AssignmentDate = assignment.AssignmentDate,
                AssignmentStatus = assignment.AssignmentStatus,
                HRAssignId = assignment.HRAssignId,
                HRAssigns = assignment.HRAssigns.FullName,
                AssignmentInspectionDate = DateTime.Today,
                DepartmentId = assignment.DepartmentId,
                Department = assignment.Department.DepartmentName,
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
                    assignment.AssignmentId = model.Id;
                    assignment.LMInspectId = model.LMInspectId;
                    assignment.LMAssignId = model.LMAssignId;
                    assignment.AssignmentDate = model.AssignmentDate;
                    assignment.AssignmentStatus = model.AssignmentStatus;
                    assignment.PCId = model.PCId;
                    assignment.HRAssignId = model.HRAssignId;
                    assignment.DepartmentId = model.DepartmentId;
                }
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// This action deletes assignments for Line managers
        /// </summary>
        /// <param name="int", ></param>
        /// <returns>Assignment, Delete view</returns>
        // GET: Assignment/Delete/5
        [Authorize(Roles = "HRAssociate")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
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
                LMInspectId = assignment.LMInspectId,
                PCId = assignment.PCId,
                ProbationaryColleague = assignment.ProbationaryColleague.FullName,
                LMAssignId = assignment.LMAssignId, // Include Ids in get as well as the properties. include hidden id in view
                LMAssigned = assignment.LMAssigned.FullName,
                AssignmentDate = assignment.AssignmentDate,
                AssignmentStatus = assignment.AssignmentStatus,
                HRAssignId = assignment.HRAssignId,
                HRAssigns = assignment.HRAssigns.FullName,
                AssignmentInspectionDate = assignment.AssignmentInspectionDate,
                DepartmentId = assignment.DepartmentId,
                Department = assignment.Department.DepartmentName,
            };

            return View(model);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCountAssignmentsPartial()
        {
            // Modify the condition inside the Count() to suite your needs
            int count = db.Assignments/*.Where(l=>l.LMAssignId ==  User.Identity.GetUserId<int>())*/.Count(p => p.AssignmentStatus == Assignment.AssignStatus.Pending);
            return PartialView(count);
        }
    }
}
