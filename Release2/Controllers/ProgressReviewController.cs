using Microsoft.AspNet.Identity;
using Project._1.Models;
using Release2.Models;
using Release2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Release2.Controllers
{
    public class ProgressReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProgressReview
        public ActionResult Index()
        {
            var review = db.ProgressReviews.ToList();

            var model = new List<ProgressReviewViewModel>();
            foreach (var item in review)
            {
                model.Add(new ProgressReviewViewModel
                {
                    Id = item.ReviewId,
                    ProbationaryColleague = item.ProbationaryColleague.FullName,
                    LMCreates = item.LMCreates.FullName,
                    PRCompletionStatus = item.PRCompletionStatus,
                    PRHRAEvalDecision = item.PRHRAEvalDecision,
                    PREvalDescription = item.PREvalDescription,
                    SelfEvaluation = item.SelfEvaluation,
                    AssessmentStatus = item.AssessmentStatus,
                });
            }

            return View(model);
        }

        // GET: ProgressReview/Details/5
        //[Authorize(Roles = "HR Associate, Department Head, Line Manager, Probationary Colleague")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgressReview review = db.ProgressReviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            var model = new ProgressReviewViewModel
            {
                Id = review.ReviewId,
                EvalDescription = review.EvalDescription,
                TotalScore = review.TotalScore,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                PRCompletionStatus = review.PRCompletionStatus,
                PRDHApprovalStatus = review.PRDHApprovalStatus,
                PRHRAEvalDecision = review.PRHRAEvalDecision,
                PRSubmissionDate = review.PRSubmissionDate,
                SASubmissionDate = review.SASubmissionDate,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                LMCreates = review.LMCreates.FullName,

            };

            return View(model);
        }

        // GET: ProgressReview/Create
        [Authorize(Roles = "LineManager")]
        public ActionResult Create()
        {
            //var colleagueDep = db.Colleagues.Where(c => c.Id == model.LMId).Select(s => s.DepartmentId).Single();
            //ViewBag.PCId = new SelectList(db.ProbationaryColleagues.Select(s => s.DepartmentId == colleagueDep), "Id", "Username");

            // create performance scores view model
            var model = new ProgressReviewViewModel();
            {
                foreach (var item in db.Competencies.ToList())
                {
                    model.Competencies.Add(
                        new CompetencyViewModel
                        {
                             Id = item.CompetencyId,
                             CompetencyName = item.CompetencyName,
                        });
                }

                var review = new ProgressReview
                {
                    ReviewId = model.Id,
                    PCId = model.PCId,
                    LMId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,

                };

            }
            // Gives error that 'Id' does not exist in this viewbag.

            var LMDep = db.Colleagues.Where(c => c.Id == model.LMId).Select(s => s.DepartmentId).SingleOrDefault();
            ViewBag.PCId = new SelectList(db.ProbationaryColleagues.Where(s => s.DepartmentId == LMDep), "Id", "Username");
            return View(model);
        }

        // POST: ProgressReview/Create
        [HttpPost]
        public ActionResult Create(ProgressReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the review from the model
                var review = new ProgressReview
                {
                    ReviewId = model.Id,
                    PCId = model.PCId,
                    LMId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    EvalDescription = model.EvalDescription,
                    TotalScore = db.PerformanceCriterions.Where(r => r.ReviewId == model.Id).Sum(g => g.Score),
                    PRCompletionStatus = ProgressReview.CompletionStatus.Incomplete,
                    AssessmentStatus = ProgressReview.Status.Pending,
                    PRHRAEvalDecision = ProgressReview.EvaluationDecision.Pending,
                    PRDHApprovalStatus = ProgressReview.ApprovalStatus.Pending,
                    PRSubmissionDate = DateTime.Today
                };

                //Save the created review to the database
                db.ProgressReviews.Add(review);
                db.SaveChanges();

                //var totalScore = db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).Sum(g => g.Score); // where to display
                //    var performances = db.PerformanceCriterions.Where(p => p.ReviewId == id).ToList();
                //int counter = 0;
                foreach (var item in model.Competencies)
                {
                    var criteria = new PerformanceCriterion
                    {
                        ReviewId = review.ReviewId,
                        CompetencyId = item.Id,
                        Score = item.Score,
                        //Score = model.Scores[counter]
                    };

                    //counter++;
                    db.PerformanceCriterions.Add(criteria);
                    //db.SaveChanges();
                }

                db.SaveChanges();
               // Utilities.SendEmail("elaf");
                return RedirectToAction("Index");
            }
            var colleagueDep = db.Colleagues.Where( c => c.Id == model.LMId).Select( s => s.DepartmentId).SingleOrDefault();
            ViewBag.PCId = new SelectList(db.ProbationaryColleagues.Where(s => s.DepartmentId == colleagueDep), "Id", "Username");

            return View(model);
        }

        //// POST: ProgressReview/CreatePerformanceReview
        //[HttpPost]
        ////[Authorize(Roles = "Line Manager")]
        //public PartialViewResult CreatePerformanceReview(int? id) //(PerformanceCriterionViewModel model )
        //{
        //    var totalScore = db.PerformanceCriterions.Where(r => r.ReviewId == id).Sum(g => g.Score);
        //    var performances = db.PerformanceCriterions.Where(p => p.ReviewId == id).ToList();
        //    var model = new List<PerformanceCriterionViewModel>();
        //    foreach (var performance in performances)
        //    {
        //        model.Add(new PerformanceCriterionViewModel
        //        {
        //            Id = performance.ReviewId,
        //            CompetencyId = performance.CompetencyId,
        //            CompetencyName = performance.Competency.CompetencyName,
        //            Score = performance.Score,
        //            TotalScore = totalScore
        //        });
        //    }

        //    return PartialView(model);
        //}

        // GET: ProgressReview/Assess/5
        [Authorize(Roles = "ProbationaryColleague")]
        public ActionResult Assess(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProgressReview review = db.ProgressReviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            ProgressReviewViewModel model = new ProgressReviewViewModel
            {
                Id = review.ReviewId,
                PRCompletionStatus = ProgressReview.CompletionStatus.Complete,
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                PRDHApprovalStatus = ProgressReview.ApprovalStatus.Pending,
                PRHRAEvalDecision = ProgressReview.EvaluationDecision.Pending,
                TotalScore = review.TotalScore,
                EvalDescription = review.EvalDescription
            };


            // add performance to all
            foreach (var item in db.PerformanceCriterions.ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
                        
                    });
            }

            // use index template to show competency scores

            var performance = db.PerformanceCriterions.ToList();

            var model1 = new List<PerformanceCriterionViewModel>();
            foreach (var item in performance)
            {
                model1.Add(new PerformanceCriterionViewModel
                {
                    Id = item.ReviewId,
                    CompetencyId = item.CompetencyId,
                    CompetencyName = item.Competency.CompetencyName,
                    Score = item.Score
                });
            }

            return View(model);
        }

        // POST: ProgressReview/Assess/5
        [HttpPost]
        public ActionResult Assess(int id, FormCollection collection, ProgressReviewViewModel model )
        {
            if (ModelState.IsValid)
            {
                var review = db.ProgressReviews.Find(id);
                if (review != null)
                {
                    review.EvalDescription = model.EvalDescription;
                    review.TotalScore = model.TotalScore;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.AssessmentStatus = ProgressReview.Status.Submitted;
                    review.SASubmissionDate = DateTime.Today; // add jquery datetime type
                    review.CreationPCId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
                   
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
                return View();
            }
        }

        // GET: ProgressReview/Edit/5
        [Authorize(Roles = "LineManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProgressReview review = db.ProgressReviews.Find(id);
            //ProgressReview review = db.ProgressReviews.Find(id).PRDHApprovalStatus== ProgressReview.ApprovalStatus.Approved;
            if (review == null)
            {
                return HttpNotFound();
            }

            ProgressReviewViewModel model = new ProgressReviewViewModel
            {
                Id = review.ReviewId,
                PRCompletionStatus = review.PRCompletionStatus,
                AssessmentStatus = review.AssessmentStatus,
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                EvalDescription = review.EvalDescription
            };

            foreach (var item in db.Competencies.ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.CompetencyName,
                    });
            }

            // use index template to show competency scores

            var performance = db.PerformanceCriterions.ToList();
            var model1 = new List<PerformanceCriterionViewModel>();
            foreach (var item in performance)
            {
                model1.Add(new PerformanceCriterionViewModel
                {
                    Id = item.ReviewId,
                    CompetencyId = item.CompetencyId,
                    CompetencyName = item.Competency.CompetencyName,
                    Score = item.Score
                });
            }
            ViewBag.PCId = new SelectList(db.ProbationaryColleagues, "Id", "Username");

            return View(model);
        }

        // POST: ProgressReview/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, ProgressReviewViewModel model)
        {
            if (ModelState.IsValid)
            {

                var review = db.ProgressReviews.Find(id);
                if (review != null)
                {
                    review.EvalDescription = model.EvalDescription;
                    review.TotalScore = model.TotalScore;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.CreationPCId = model.CreationPCId;
                    review.AssessmentStatus = model.AssessmentStatus;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.SASubmissionDate = model.SASubmissionDate;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRDHApproveDate = model.PRDHApproveDate;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PRHRAEvalDate = model.PRHRAEvalDate;
                    review.PRDHApprovesId = model.PRDHApprovesId;
                    review.HREvaluatesId = model.HREvaluatesId;

                    //foreach (var performance in model.PerformanceReviews)
                    //{
                    //    //model.p.Add(
                    //    //    new CompetencyViewModel
                    //    //    {
                    //    performance.CompetencyId;
                    //    performance.Score;
                    //        //});
                    //}

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
                ViewBag.PCId = new SelectList(db.ProbationaryColleagues, "Id", "Username");
                return View();
            }
        }

        // GET: ProgressReview/Approve/5
        [Authorize(Roles = "DepartmentHead")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProgressReview review = db.ProgressReviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            ProgressReviewViewModel model = new ProgressReviewViewModel
            {
                Id = review.ReviewId,
                PRCompletionStatus = review.PRCompletionStatus,
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                PRDHApprovalStatus = review.PRDHApprovalStatus,
                PRDHApprovesId= User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                DHApproval = review.DHApproval.FullName,
                PRDHApproveDate = review.PRDHApproveDate.Value.Date,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                AssessmentStatus = review.AssessmentStatus,
                SASubmissionDate = review.SASubmissionDate,
                CreationPCId = review.CreationPCId,
                CreationPC = review.CreationPC.FullName,
            };

            // use index template to show competency scores

            var performance = db.PerformanceCriterions.ToList();
            var model1 = new List<PerformanceCriterionViewModel>();
            foreach (var item in performance)
            {
                model1.Add(new PerformanceCriterionViewModel
                {
                    Id = item.ReviewId,
                    CompetencyId = item.CompetencyId,
                    CompetencyName = item.Competency.CompetencyName,
                    Score = item.Score
                });
            }

            return View(model);
        }

        // POST: ProgressReview/Approve/5
        [HttpPost]
        public ActionResult Approve(int id, FormCollection collection, ProgressReviewViewModel model)
        {
            if (ModelState.IsValid)
            {

                var review = db.ProgressReviews.Find(id);
                if (review != null)
                {
                    review.EvalDescription = model.EvalDescription;
                    review.TotalScore = model.TotalScore;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.CreationPCId = model.CreationPCId;
                    review.AssessmentStatus = model.AssessmentStatus;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.SASubmissionDate = model.SASubmissionDate;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRDHApproveDate = model.PRDHApproveDate;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PRHRAEvalDate = model.PRHRAEvalDate;
                    review.PRDHApprovesId = model.PRDHApprovesId;

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
                return View();
            }
        }

        // GET: ProgressReview/Evaluate/5
        [Authorize(Roles = "HRAssociate")]
        public ActionResult Evaluate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProgressReview review = db.ProgressReviews.Find(id);
            //ProgressReview review = db.ProgressReviews.Find(id).PRDHApprovalStatus== ProgressReview.ApprovalStatus.Approved;
            if (review == null)
            {
                return HttpNotFound();
            }

            ProgressReviewViewModel model = new ProgressReviewViewModel
            {
                Id = review.ReviewId,
                PRCompletionStatus = review.PRCompletionStatus,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                AssessmentStatus = review.AssessmentStatus,
                LMId = review.LMId,
                SASubmissionDate = review.SASubmissionDate,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                CreationPCId = review.CreationPCId,
                CreationPC = review.CreationPC.FullName,
                PRDHApprovalStatus = review.PRDHApprovalStatus,
                PRDHApprovesId = review.PRDHApprovesId,
                DHApproval = review.DHApproval.FullName,
                PRDHApproveDate = review.PRDHApproveDate,
                PRHRAEvalDate = DateTime.Today,
                PRHRAEvalDecision = review.PRHRAEvalDecision,
                HREvaluatesId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                HREvaluation = review.HREvaluation.FullName,
            };

            // use index template to show competency scores

            var performance = db.PerformanceCriterions.ToList();
            var model1 = new List<PerformanceCriterionViewModel>();
            foreach (var item in performance)
            {
                model1.Add(new PerformanceCriterionViewModel
                {
                    Id = item.ReviewId,
                    CompetencyId = item.CompetencyId,
                    CompetencyName = item.Competency.CompetencyName,
                    Score = item.Score
                });
            }

            return View(model);
        }

        // POST: ProgressReview/Evaluate/5
        [HttpPost]
        //[Authorize(Roles = "Probationary Colleague")]
        public ActionResult Evaluate(int id, FormCollection collection, ProgressReviewViewModel model)
        {
            if (ModelState.IsValid)
            {

                var review = db.ProgressReviews.Find(id);
                if (review != null)
                {
                    review.EvalDescription = model.EvalDescription;
                    review.TotalScore = model.TotalScore;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.CreationPCId = model.CreationPCId;
                    review.AssessmentStatus = model.AssessmentStatus;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.SASubmissionDate = model.SASubmissionDate;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRDHApproveDate = model.PRDHApproveDate;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PRHRAEvalDate = model.PRHRAEvalDate;
                    review.PRDHApprovesId = model.PRDHApprovesId;
                    review.HREvaluatesId = model.HREvaluatesId;

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
                ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
                return View();
            }
        }
    }
}