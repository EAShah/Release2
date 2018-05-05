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
        /// <summary>
        /// This action returns a list of Progress Reviews based appropriate to the user in role
        /// </summary>
        /// <returns>Progress Review, Index view</returns>
        // GET: ProgressReview
        public ActionResult Index()
        {
            var userid = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var review = db.ProgressReviews./*Where(p => p.LMId == userid || p.PCId == userid).*/ToList();

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

        /// <summary>
        /// This action returns details of Progress Reviews 
        /// </summary>
        /// <returns>Progress Review, Details view</returns>

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
            // use index template to show competency scores
            foreach (var item in db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
                    });
            }

            return View(model);
        }

        /// <summary>
        /// This action allows Line Managers to create Progress Reviews 
        /// </summary>
        /// <returns>Progress Review, Create view</returns>
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

                ViewBag.PCId = new SelectList(db.Assignments.Where(l => l.LMAssignId == review.LMId).Select(p => p.ProbationaryColleague), "Id", "Fullname");
            }
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
                    TotalScore = model.TotalScore,
                    SelfEvaluation = model.SelfEvaluation,
                    PREvalDescription = model.PREvalDescription,
                    PRCompletionStatus = ProgressReview.CompletionStatus.Incomplete,
                    AssessmentStatus = ProgressReview.Status.Pending,
                    PRSubmissionDate = DateTime.Today
                };

                //Save the created review to the database
                db.ProgressReviews.Add(review);
                db.SaveChanges();

                foreach (var item in model.Competencies)
                {
                    var criteria = new PerformanceCriterion
                    {
                        ReviewId = review.ReviewId,
                        CompetencyId = item.Id,
                        Score = item.Score,
                    };

                    db.PerformanceCriterions.Add(criteria);
                    //db.SaveChanges();
                }

                db.SaveChanges();
                var tscore = db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).Select(s => s.Score).Sum();
                review.TotalScore = tscore;
                db.SaveChanges();
                 return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// This action counts Progress Reviews for Probationary Colleagues 
        /// </summary>
        /// <returns>Progress Review badge</returns>
        public ActionResult GetCountReviewsToAssessPartial()
        {
            // Modify the condition inside the Count() to suite your needs
            int count = db.ProgressReviews/*.Where(p=>p.PCId == User.Identity.GetUserId<int>())*/.Count(p => p.AssessmentStatus == ProgressReview.Status.Pending);
            return PartialView(count);
        }

        /// <summary>
        /// This action allows Probationary Colleagues to assess Progress Reviews 
        /// </summary>
        /// <param name="int", ></param>
        /// <returns>Progress Review, Assess view</returns>
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
                TotalScore = review.TotalScore,//db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).Select(s => s.Score).Sum(),
                EvalDescription = review.EvalDescription,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation, 
                SASubmissionDate = review.SASubmissionDate
            };

            // use index template to show competency scores
            foreach (var item in db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
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
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.AssessmentStatus = ProgressReview.Status.Submitted;
                    review.SASubmissionDate = DateTime.Today; // add jquery datetime type
                    review.CreationPCId =  User.Identity.GetUserId<int>();
                   
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

        /// <summary>
        /// This action allows Line Managers to edit Progress Reviews 
        /// </summary>
        /// <param name="int", ></param>
        /// <returns>Progress Review, Edit view</returns>
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
                EvalDescription = review.EvalDescription,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                TotalScore = review.TotalScore
            };

            // use index template to show competency scores
            foreach (var item in db.PerformanceCriterions.Where(r=>r.ReviewId== review.ReviewId).ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
                    });
            }

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
                    //review.LMId = model.LMId;
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


                    foreach (var item in model.Competencies)
                    {
                        var criteria = new PerformanceCriterion
                        {
                            ReviewId = review.ReviewId,
                            CompetencyId = item.Id,
                            Score = item.Score,
                        };

                        db.Entry(criteria).State = System.Data.Entity.EntityState.Modified;
                        //db.PerformanceCriterions.Add(criteria);
                    }
                    db.SaveChanges();
                    var tscore = db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).Select(s => s.Score).Sum();
                    review.TotalScore = tscore;
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

        /// <summary>
        /// This action counts Progress Reviews for Department Heads 
        /// </summary>
        /// <returns>Progress Review badge</returns>
        public ActionResult GetCountReviewsToApprovePartial()
        {
            // Modify the condition inside the Count() to suite your needs
            int count = db.ProgressReviews.Count(p => p.PRDHApprovalStatus == ProgressReview.ApprovalStatus.Pending);
            return PartialView(count);
        }

        /// <summary>
        /// This action allows Department Heads to approve Progress Reviews 
        /// </summary>
        /// <param name="int", ></param>
        /// <returns>Progress Review, Approve view</returns>
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
                PRDHApprovesId= review.PRDHApprovesId,
                //DHApproval = review.DHApproval,
                PRDHApproveDate = review.PRDHApproveDate,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                AssessmentStatus = review.AssessmentStatus,
                SASubmissionDate = review.SASubmissionDate,
                EvalDescription = review.EvalDescription,
                TotalScore = review.TotalScore,
                CreationPCId = review.CreationPCId,
                //CreationPC = review.CreationPC.FullName,
                PRHRAEvalDecision = review.PRHRAEvalDecision,
            };

            // use index template to show competency scores
            foreach (var item in db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        ReviewId = review.ReviewId,
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
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
                    //review.TotalScore = model.TotalScore;
                    review.EvalDescription = model.EvalDescription;
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.CreationPCId = model.CreationPCId;
                    review.AssessmentStatus = model.AssessmentStatus;
                    review.PRCompletionStatus = model.PRCompletionStatus;
                    review.SASubmissionDate = model.SASubmissionDate;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRDHApproveDate = DateTime.Today;
                    review.PRDHApprovesId = User.Identity.GetUserId<int>();
                    review.PRHRAEvalDecision = ProgressReview.EvaluationDecision.Pending;

                    //db.Entry(review).State = System.Data.Entity.EntityState.Modified;

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

            /// <summary>
            /// This action counts Progress Reviews for HR Associates 
            /// </summary>
            /// <returns>Progress Review badge</returns>
            public ActionResult GetCountReviewsToEvaluatePartial()
        {
            // Modify the condition inside the Count() to suite your needs
            int count = db.ProgressReviews.Count(p => p.PRHRAEvalDecision == ProgressReview.EvaluationDecision.Pending);
            return PartialView(count);
        }

        /// <summary>
        /// This action allows HR Associates to evaluate Progress Reviews 
        /// </summary>
        /// <param name="int", ></param>
        /// <returns>Progress Review, Approve view</returns>
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
                LMId = review.LMId,
                LMCreates = review.LMCreates.FullName,
                PCId = review.PCId,
                ProbationaryColleague = review.ProbationaryColleague.FullName,
                HREvaluatesId = review.HREvaluatesId,
                PRDHApprovalStatus = review.PRDHApprovalStatus,
                PRDHApprovesId = review.PRDHApprovesId,
                //DHApproval = review.DHApproval,
                PRDHApproveDate = review.PRDHApproveDate,
                PREvalDescription = review.PREvalDescription,
                SelfEvaluation = review.SelfEvaluation,
                AssessmentStatus = review.AssessmentStatus,
                SASubmissionDate = review.SASubmissionDate,
                EvalDescription = review.EvalDescription,
                TotalScore = review.TotalScore,
                CreationPCId = review.CreationPCId,
                //CreationPC = review.CreationPC.FullName,
                PRHRAEvalDecision = review.PRHRAEvalDecision,

            };

            // use index template to show competency scores
            foreach (var item in db.PerformanceCriterions.Where(r => r.ReviewId == review.ReviewId).ToList())
            {
                model.Competencies.Add(
                    new CompetencyViewModel
                    {
                        Id = item.CompetencyId,
                        CompetencyName = item.Competency.CompetencyName,
                        Score = item.Score,
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
                    review.PREvalDescription = model.PREvalDescription;
                    review.SelfEvaluation = model.SelfEvaluation;
                    review.LMId = model.LMId;
                    review.PCId = model.PCId;
                    review.CreationPCId = model.CreationPCId;
                    review.AssessmentStatus = model.AssessmentStatus;
                    review.PRCompletionStatus = ProgressReview.CompletionStatus.Complete;
                    review.SASubmissionDate = model.SASubmissionDate;
                    review.PRDHApprovalStatus = model.PRDHApprovalStatus;
                    review.PRDHApproveDate = model.PRDHApproveDate;
                    review.PRHRAEvalDecision = model.PRHRAEvalDecision;
                    review.PRHRAEvalDate = DateTime.Today;
                    review.PRDHApprovesId = model.PRDHApprovesId;
                    review.HREvaluatesId = User.Identity.GetUserId<int>() ;

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