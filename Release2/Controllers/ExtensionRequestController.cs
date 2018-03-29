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
    public class ExtensionRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtensionRequest
        public ActionResult AllIndex()
        {
            var extension = db.ExtensionRequests.ToList();
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extension)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    ExtRequestStatus = item.ExtRequestStatus,  // ADD PENDING, APPROVED REJECTED AND ALL(done) EXT VIEWS
                    LMSubmits = item.LMSubmits.FullName,
                    ExtendedPC = item.ExtendedPC.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate
                });
            }
            return View(model);
        }

        // GET: ExtensionRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExtensionRequest/Create
        public ActionResult Create()
        {
            var list = db.ProbationaryColleagues.Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ExtendedPCID = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FullName");
            return View();
        }

        // POST: ExtensionRequest/Create
        [HttpPost]
        public ActionResult Create(ExtensionRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var probationaryColleague = db.ProbationaryColleagues.Select(p => p.FirstName);
                var request = new ExtensionRequest
                {
                    ExtRequestId = model.Id,
                    ExtNumber = model.ExtNumber,
                    ExtReason = model.ExtReason,
                    ExtendedPCID = model.ExtendedPCID,
                    LMSubmitID = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    ExtRequestSubmissionDate= DateTime.Now,
                    ExtRequestStatus = ExtensionRequest.RequestStatus.Pending
                };

                db.ExtensionRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ExtendedPCID = new SelectList(db.ProbationaryColleagues, "ColleagueId", "FullName");
                return View(model);
            }
        }

        // GET: ExtensionRequest/Audit/5
        public ActionResult Audit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ExtensionRequest extension = db.ExtensionRequests.Find(id);
            if (extension == null)
            {
                return HttpNotFound();
            }

            ExtensionRequestViewModel model = new ExtensionRequestViewModel
            {
                Id = extension.ExtRequestId,
                ExtendedPC = extension.ExtendedPC.FullName,
                LMSubmits = extension.LMSubmits.FullName,
                ExtRequestSubmissionDate = extension.ExtRequestSubmissionDate,
                ExtRequestStatus = extension.ExtRequestStatus,
                HRAudits = extension.HRAudits.FullName,
                ExtRequestAuditDate = DateTime.Now
            };

            return View(model);
        }

        // POST: ExtensionRequest/Audit/5
        [HttpPost]
        public ActionResult Audit(int id, ExtensionRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var extension = db.ExtensionRequests.Find(id);
                if (extension != null)
                {
                    extension.HRAuditID = model.HRAuditID;
                    extension.ExtRequestSubmissionDate = model.ExtRequestSubmissionDate;
                    extension.ExtReason = model.ExtReason;
                    extension.ExtNumber = model.ExtNumber;
                    extension.ExtRequestStatus = model.ExtRequestStatus;
                    extension.ExtendedPCID = model.ExtendedPCID;
                    extension.LMSubmitID = model.LMSubmitID;
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
