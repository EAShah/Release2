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

//namespace Release2.Controllers
//{
//    public class ProgressReviewController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: ProgressReview
//        public ActionResult Index()
//        {
           
//            var review = db.ProgressReviews.ToList();

//            var model = new List<ProgressReviewViewModel>();
//            foreach (var item in review)
//            {
//                model.Add(new ProgressReviewViewModel
//                {
//                    Id = item.ReviewId,
//                    //EvalDescription = item.EvalDescription,
//                    //TotalGrade = item.TotalGrade,
//                    //PREvalDescription = item.PREvalDescription,
//                    //SelfEvaluation = item.SelfEvaluation,
//                    PRCompletionStatus = item.PRCompletionStatus,
//                    PRHRAEvalDecision = item.PRHRAEvalDecision,
//                    //SACompletionStatus = item.SACompletionStatus,
//                    //SAApprovalStatus = item.SAApprovalStatus,
//                    //SAHRAReviewDecision = item.SAHRAReviewDecision
//                });
//            }
//            return View(model);
//        }

//        // GET: ProgressReview/Details/5
//        //[Authorize(Roles = "HR Associate, Department Head, Line Manager, Probationary Colleague")]
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProgressReview review = db.ProgressReviews.Find(id);
//            if (review == null)
//            {
//                return HttpNotFound();
//            }

//            var model = new ProgressReviewViewModel
//            {
//                Id = review.ReviewId,
//                EvalDescription = review.EvalDescription,
//                TotalGrade = review.TotalGrade,
//                //PREvalDescription = review.PREvalDescription,
//                //SelfEvaluation = review.SelfEvaluation,
//                PRCompletionStatus = review.PRCompletionStatus,
//                PRHRAEvalDecision = review.PRHRAEvalDecision,
//                //SACompletionStatus = review.SACompletionStatus,
//                //SAApprovalStatus = review.SAApprovalStatus,
//                //SAHRAReviewDecision = review.SAHRAReviewDecision
//            };

//            return View(model);
//        }

//        // GET: ProgressReview/Create
//        //[Authorize(Roles ="Line Manager")]
//        public ActionResult Create()
//        {
//            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
//            ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
//            ViewBag.DepartmentId = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FirstName LastName");
//            return View();
//        }

//        // POST: ProgressReview/Create
//        [HttpPost]
//        //[Authorize(Roles = "Line Manager")]
//        public ActionResult Create(ProgressReviewViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Create the review from the model
//                var review = new ProgressReview
//                {
//                    EvalDescription = model.EvalDescription,
//                    TotalGrade = model.TotalGrade,
//                };

//                //Save the created review to the database
//                db.ProgressReviews.Add(review);
//                db.SaveChanges();
//            }

//            else
//            {
//                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
//                ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
//                ViewBag.DepartmentId = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FirstName LastName");

//                return RedirectToAction("Index");
//            }

//            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
//            ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
//            ViewBag.DepartmentId = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FirstName LastName");

//            return View();
//        }


//        // GET: ProgressReview/SelfAssessment/5
//        //[Authorize(Roles = "Probationary Colleague")]
//        public ActionResult SelfAssessment(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            ProgressReview review = db.ProgressReviews.Find(id);
//            if (review == null)
//            {
//                return HttpNotFound();
//            }

//            ProgressReviewViewModel model = new ProgressReviewViewModel
//            {
//                Id = review.ReviewId,
//                EvalDescription = review.EvalDescription,
//                TotalGrade = review.TotalGrade,
//                PREvalDescription = review.PREvalDescription,
//                SelfEvaluation = review.SelfEvaluation,
//            };

//            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
//            ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
//            return View(model);
//        }

//        // POST: ProgressReview/SelfAssessment/5
//        [HttpPost]
//        //[Authorize(Roles = "Probationary Colleague")]
//        public ActionResult SelfAssessment(int id, FormCollection collection, ProgressReviewViewModel model)
//        {
//            if (ModelState.IsValid)
//            {

//                var review = db.ProgressReviews.Find(id);
//                if (review != null)
//                {
//                    review.EvalDescription = model.EvalDescription;
//                    review.TotalGrade = model.TotalGrade;
//                    review.PREvalDescription = model.PREvalDescription;
//                    review.SelfEvaluation = model.SelfEvaluation;

//                    db.SaveChanges();

//                    return RedirectToAction("Index");
//                }
//                else
//                {
//                    return HttpNotFound();
//                }
//            }
//            else
//            {
//                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
//                ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
//                return View();
//            }
//        }

//        // GET: ProgressReview/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProgressReview review = db.ProgressReviews.Find(id);
//            if (review == null)
//            {
//                return HttpNotFound();
//            }
//            ProgressReviewViewModel model = new ProgressReviewViewModel
//            {
//                Id = review.ReviewId,
//                EvalDescription = review.EvalDescription,
//                TotalGrade = review.TotalGrade,
//                //PREvalDescription = review.PREvalDescription,
//                //SelfEvaluation = review.SelfEvaluation,
//                PRCompletionStatus = review.PRCompletionStatus,
//                PRHRAEvalDecision = review.PRHRAEvalDecision,
//                //SACompletionStatus = review.SACompletionStatus,
//                //SAApprovalStatus = review.SAApprovalStatus,
//                //SAHRAReviewDecision = review.SAHRAReviewDecision
//            };

//            return View(model);
//        }

//        // POST: ProgressReview/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ProgressReview review = db.ProgressReviews.Find(id);
//            db.ProgressReviews.Remove(review);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//    }
//}