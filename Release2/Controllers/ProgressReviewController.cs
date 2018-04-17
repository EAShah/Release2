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
                    SelfAssessmentId = item.SelfAssessmentId

                });
            }

            var assessment = db.SelfAssessments.ToList();

            var model1 = new List<SelfAssessmentViewModel>();
            foreach (var item in assessment)
            {
                model1.Add(new SelfAssessmentViewModel
                {
                    Id = item.AssessmentId,
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
                PRCompletionStatus = review.PRCompletionStatus,
                PRDHApprovalStatus = review.PRDHApprovalStatus,
                PRHRAEvalDecision = review.PRHRAEvalDecision,
            };

            return View(model);
        }

        // GET: ProgressReview/Create
        //[Authorize(Roles ="Line Manager")]
        public ActionResult Create()
        {
            ViewBag.PCId = new SelectList(db.ProbationaryColleagues, "Id", "Username");

            // create performance scores view model
            var model1 = new ProgressReviewViewModel();
            {
                foreach (var item in db.Competencies.ToList())
                {
                    model1.Competencies.Add(
                        new CompetencyViewModel
                        {
                             Id = item.CompetencyId,
                              CompetencyName = item.CompetencyName,
                        });
                }
                
            }
            // get dptid of lm and get all the colleagues with that deptid
            //ViewBag.PCId = new SelectList(db.ProbationaryColleagues.Where(p=>p.DepartmentId == LMId.DeparmentId), "Id", "UserName");
            return View(model1);
        }

        // POST: ProgressReview/Create
        [HttpPost]
        //[Authorize(Roles = "Line Manager")]
        public ActionResult Create(ProgressReviewViewModel model, SelfAssessmentViewModel model1)
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
                    //AssessmentStatus = SelfAssessment.Status.Pending,
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
                    db.SaveChanges();
                }

                //db.SaveChanges();
                //SendEmail()
            }
            else
            {
                ViewBag.PCId = new SelectList(db.ProbationaryColleagues, "Id", "Username");

                return RedirectToAction("Index");
            }

            ViewBag.PCId = new SelectList(db.ProbationaryColleagues, "Id", "Username");

            return View();
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
        //[Authorize(Roles = "Probationary Colleague")]
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
                SelfAssessmentId = review.SelfAssessmentId,
                PRCompletionStatus = ProgressReview.CompletionStatus.Complete,
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
               PRDHApprovalStatus = ProgressReview.ApprovalStatus.Pending,
                PRHRAEvalDecision = ProgressReview.EvaluationDecision.Pending,
                TotalScore = review.TotalScore
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

        // POST: ProgressReview/Assess/5
        [HttpPost]
        //[Authorize(Roles = "Probationary Colleague")]
        public ActionResult Assess(int id, FormCollection collection, ProgressReviewViewModel model, SelfAssessmentViewModel model1)
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
                    review.SelfAssessmentId = model.SelfAssessmentId;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;

                    var assessment = new SelfAssessment
                    {
                        PREvalDescription = model1.PREvalDescription,
                        SelfEvaluation = model1.SelfEvaluation,
                        AssessmentStatus = SelfAssessment.Status.Submitted,
                        SASubmissionDate = DateTime.Today, // add jquery datetime type
                        CreationPCId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    }; 

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

        // GET: ProgressReview/Approve/5
        //[Authorize(Roles = "Department Head")]
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
                SelfAssessmentId = review.SelfAssessmentId,
                PRCompletionStatus = review.PRCompletionStatus,
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                 PRDHApprovalStatus = review.PRDHApprovalStatus,
                 PRDHApprovesId= User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                 DHApproval = review.DHApproval.FullName,
                PRDHApproveDate = review.PRDHApproveDate.Value.Date, 
            };

            SelfAssessmentViewModel model1 = new SelfAssessmentViewModel
            {
                Id = review.SelfAssessment.AssessmentId,
                PREvalDescription = review.SelfAssessment.PREvalDescription,
                SelfEvaluation = review.SelfAssessment.SelfEvaluation,
                AssessmentStatus = review.SelfAssessment.AssessmentStatus,
                SASubmissionDate = review.SelfAssessment.SASubmissionDate,
                CreationPCId = review.SelfAssessment.CreationPCId,
                CreationPC = review.SelfAssessment.CreationPC.FullName,
            };

            return View(model);
        }

        //// POST: ProgressReview/Approve/5
        //[HttpPost]
        ////[Authorize(Roles = "Probationary Colleague")]
        //public ActionResult Approve(int id, FormCollection collection, ProgressReviewViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var review = db.ProgressReviews.Find(id);
        //        if (review != null)
        //        {
        //            review.EvalDescription = model.EvalDescription;
        //            review.TotalScore = model.TotalScore;
        //            review.PREvalDescription = model.PREvalDescription;
        //            review.SelfEvaluation = model.SelfEvaluation;
        //            review.LMId = model.LMId;
        //            review.PCId = model.PCId;
        //            review.CreationPCId = model.CreationPCId;
        //            review.SelfAssessmentId = model.SelfAssessmentId;
        //            review.AssessmentStatus = model.AssessmentStatus;
        //            review.PRCompletionStatus = model.PRCompletionStatus;
        //            review.SASubmissionDate = model.SASubmissionDate;
        //            review.PRDHApprovalStatus = model.PRDHApprovalStatus;
        //            review.PRDHApproveDate = model.PRDHApproveDate;
        //            review.PRHRAEvalDecision = model.PRHRAEvalDecision;
        //            review.PRHRAEvalDate = model.PRHRAEvalDate;
        //            review.PRDHApprovesId = model.PRDHApprovesId;

        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
        //        return View();
        //    }
        //}

        //// GET: ProgressReview/Evaluate/5
        ////[Authorize(Roles = "Probationary Colleague")]
        //public ActionResult Evaluate(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    ProgressReview review = db.ProgressReviews.Find(id);
        //    //ProgressReview review = db.ProgressReviews.Find(id).PRDHApprovalStatus == ProgressReview.ApprovalStatus.Approved;
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ProgressReviewViewModel model = new ProgressReviewViewModel
        //    {
        //        Id = review.ReviewId,
        //        SelfAssessmentId = review.SelfAssessmentId,
        //        PRCompletionStatus = review.PRCompletionStatus,
        //        PREvalDescription = review.PREvalDescription,
        //        SelfEvaluation = review.SelfEvaluation,
        //        AssessmentStatus = review.AssessmentStatus,
        //        LMId = review.LMId,
        //        SASubmissionDate = review.SASubmissionDate,
        //        LMCreates = review.LMCreates.FullName,
        //        PCId = review.PCId,
        //        ProbationaryColleague = review.ProbationaryColleague.FullName,
        //        CreationPCId = review.CreationPCId,
        //        CreationPC = review.CreationPC.FullName,
        //        PRDHApprovalStatus = review.PRDHApprovalStatus,
        //        PRDHApprovesId = review.PRDHApprovesId,
        //        DHApproval = review.DHApproval.FullName,
        //        PRDHApproveDate = review.PRDHApproveDate,
        //        PRHRAEvalDate = DateTime.Today,
        //        PRHRAEvalDecision = review.PRHRAEvalDecision,
        //        HREvaluatesId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
        //        HREvaluation = review.HREvaluation.FullName,
        //    };

        //    return View(model);
        //}

        //// POST: ProgressReview/Evaluate/5
        //[HttpPost]
        ////[Authorize(Roles = "Probationary Colleague")]
        //public ActionResult Evaluate(int id, FormCollection collection, ProgressReviewViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var review = db.ProgressReviews.Find(id);
        //        if (review != null)
        //        {
        //            review.EvalDescription = model.EvalDescription;
        //            review.TotalScore = model.TotalScore;
        //            review.PREvalDescription = model.PREvalDescription;
        //            review.SelfEvaluation = model.SelfEvaluation;
        //            review.LMId = model.LMId;
        //            review.PCId = model.PCId;
        //            review.CreationPCId = model.CreationPCId;
        //            review.SelfAssessmentId = model.SelfAssessmentId;
        //            review.AssessmentStatus = model.AssessmentStatus;
        //            review.PRCompletionStatus = model.PRCompletionStatus;
        //            review.SASubmissionDate = model.SASubmissionDate;
        //            review.PRDHApprovalStatus = model.PRDHApprovalStatus;
        //            review.PRDHApproveDate = model.PRDHApproveDate;
        //            review.PRHRAEvalDecision = model.PRHRAEvalDecision;
        //            review.PRHRAEvalDate = model.PRHRAEvalDate;
        //            review.PRDHApprovesId = model.PRDHApprovesId;
        //            review.HREvaluatesId = model.HREvaluatesId;

        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.CompetencyId = new SelectList(db.Competencies, "CompetencyId", "CompetencyName");
        //        return View();
        //    }
        //}
    }
}