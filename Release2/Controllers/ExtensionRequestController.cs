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
    public class ExtensionRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all extension requests 
        /// </summary>
        /// <returns>ExtensionRequest, AllIndex view</returns>
        // GET: ExtensionRequest
        [Authorize(Roles = "HRAssociate, LineManager")]
        public ActionResult AllIndex()
        {
            var extensions = db.ExtensionRequests.ToList();
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extensions)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtendedPC = item.ExtendedPC.FullName,
                    LMSubmits = item.LMSubmits.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate,
                    ExtRequestStatus = item.ExtRequestStatus,
                    ExtNumber = item.ExtNumber,
                    ExtReason = item.ExtReason
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action lists extension requests based on their status
        /// </summary>
        /// <returns>ExtensionRequest, GetExtensionByStatus view</returns>
        // GET: GetExtensionByStatusPartial
        [Authorize(Roles = "HRAssociate")]
        public ActionResult GetExtensionByStatusPartial(int? id)
        {
            var extension = db.ExtensionRequests.ToList().Where(e => e.ExtRequestStatus == ExtensionRequest.RequestStatus.Approved || e.ExtRequestStatus == ExtensionRequest.RequestStatus.Rejected || e.ExtRequestStatus == ExtensionRequest.RequestStatus.Pending);
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extension)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    ExtRequestStatus = item.ExtRequestStatus,
                    LMSubmits = item.LMSubmits.FullName,
                    ExtendedPC = item.ExtendedPC.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate,
                });
            }
            return PartialView(model);
        }


        /// <summary>
        /// This action lists approved extension requests 
        /// </summary>
        /// <returns>ExtensionRequest, ApprovedIndex view</returns>
        // GET: ApprovedExtensionRequest
        [Authorize(Roles = "HRAssociate")]
        public ActionResult ApprovedIndex()
        {
            var extension = db.ExtensionRequests.ToList().Where(e => e.ExtRequestStatus == ExtensionRequest.RequestStatus.Approved);
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extension)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    ExtRequestStatus = item.ExtRequestStatus,
                    LMSubmits = item.LMSubmits.FullName,
                    ExtendedPC = item.ExtendedPC.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action lists rejected extension requests 
        /// </summary>
        /// <returns>ExtensionRequest, RejectedIndex view</returns>
        // GET: RejectedExtensionRequest
        [Authorize(Roles = "HRAssociate")]
        public ActionResult RejectedIndex()
        {
            var extension = db.ExtensionRequests.ToList().Where(e => e.ExtRequestStatus == ExtensionRequest.RequestStatus.Rejected);
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extension)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    ExtRequestStatus = item.ExtRequestStatus,
                    LMSubmitId = item.LMSubmitId,
                    ExtendedPCId = item.ExtendedPCId,
                    LMSubmits = item.LMSubmits.FullName,
                    ExtendedPC = item.ExtendedPC.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action lists pending extension requests 
        /// </summary>
        /// <returns>ExtensionRequest, RejectedIndex view</returns>
        // GET: PendingExtensionRequest
        [Authorize(Roles = "HRAssociate")]
        public ActionResult PendingIndex()
        {
            var extension = db.ExtensionRequests.ToList().Where(e => e.ExtRequestStatus == ExtensionRequest.RequestStatus.Pending);
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in extension)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    ExtRequestStatus = item.ExtRequestStatus,
                    LMSubmits = item.LMSubmits.FullName,
                    ExtendedPC = item.ExtendedPC.FullName,
                    ExtRequestSubmissionDate = item.ExtRequestSubmissionDate
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action allows to view details of extension requests
        /// </summary>
        /// <param name="id", ></param>
        /// <returns>ExtensionRequest, Details view</returns>
        // GET: ExtensionRequest/Details/5
        [Authorize(Roles = "HRAssociate, LineManager")]
        public ActionResult Details(int? id)
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

            var model = new ExtensionRequestViewModel
            {
                Id = extension.ExtRequestId,
                ExtendedPC = extension.ExtendedPC.FullName,
                LMSubmitId = extension.LMSubmitId,
                LMSubmits = extension.LMSubmits.FullName,
                ExtRequestSubmissionDate = extension.ExtRequestSubmissionDate,
                HRAudits = extension.HRAudits.FullName,
                ExtRequestAuditDate = extension.ExtRequestAuditDate,
                ExtNumber = extension.ExtNumber,
                ExtReason = extension.ExtReason,
                ExtRequestStatus = extension.ExtRequestStatus
            };

            return View(model);
        }

        /// <summary>
        /// This action allows line managers to create extension requests
        /// </summary>
        /// <param name="model", ></param>
        /// <returns>ExtensionRequest, Create view</returns>
        // GET: ExtensionRequest/Create
        [Authorize(Roles = "LineManager")]
        public ActionResult Create()
        {
            var list = db.ProbationaryColleagues.Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ExtendedPCId = new SelectList(db.ProbationaryColleagues, "Id", "FullName");
            //ViewBag.LMSubmitId = new SelectList(db.Colleagues, "Id", "FullName");
            return View();
        }

        // POST: ExtensionRequest/Create
        [HttpPost]
        public ActionResult Create(ExtensionRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var extension = new ExtensionRequest
                {
                    ExtRequestId = model.Id,
                    ExtNumber = model.ExtNumber,
                    ExtReason = model.ExtReason,
                    ExtendedPCId = model.ExtendedPCId,
                    LMSubmitId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Colleagues.Where(l => l.ColleagueType == ColleagueType.LineManager).First().Id,
                    ExtRequestSubmissionDate= DateTime.Now,
                    ExtRequestStatus = ExtensionRequest.RequestStatus.Pending
                };

                db.ExtensionRequests.Add(extension);
                db.SaveChanges(); 

                return RedirectToAction("AllIndex");
            }
            else
            {
                ViewBag.ExtendedPCId = new SelectList(db.ProbationaryColleagues, "Id", "FullName");
                return View();
            }
        }

        /// <summary>
        /// This action allows for auditing extension requests
        /// </summary>
        /// <param name="id", ></param>
        /// <param name="model", ></param>
        /// <returns>ExtensionRequest, Audit view</returns>
        // GET: ExtensionRequest/Audit/5
        [Authorize(Roles = "HRAssociate")]
        public ActionResult Audit(int? id)
        {
            // Find Extension request and edit details

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
                ExtendedPCId = extension.ExtendedPCId,
                LMSubmitId = extension.LMSubmitId,
                ExtendedPC = extension.ExtendedPC.FullName,
                LMSubmits = extension.LMSubmits.FullName,
                ExtRequestSubmissionDate = extension.ExtRequestSubmissionDate,
                ExtRequestStatus = extension.ExtRequestStatus,
                HRAudits = User.Identity.Name,
                ExtRequestAuditDate = DateTime.Now,
                ExtReason = extension.ExtReason,
                ExtNumber = extension.ExtNumber
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
                    extension.HRAuditId = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Colleagues.Where(h => h.ColleagueType == ColleagueType.HRAssociate).First().Id;
                    extension.ExtRequestSubmissionDate = model.ExtRequestSubmissionDate;
                    extension.ExtReason = model.ExtReason;
                    extension.ExtNumber = model.ExtNumber;
                    extension.ExtRequestStatus = model.ExtRequestStatus;
                    extension.ExtendedPCId = model.ExtendedPCId;
                    extension.LMSubmitId = model.LMSubmitId;

                    db.Entry(extension).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("AllIndex");
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

        /// <summary>
        /// This action allows deleting extension requests
        /// </summary>
        /// <param name="id", ></param>
        /// <returns>ExtensionRequest, Details view</returns>
        // GET: ExtensionRequest/Delete/5
        [Authorize(Roles = "LineManager")]
        public ActionResult Delete(int? id)
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

            var model = new ExtensionRequestViewModel
            {
                Id = extension.ExtRequestId,
                ExtendedPC = extension.ExtendedPC.FullName,
                LMSubmitId = extension.LMSubmitId,
                LMSubmits = extension.LMSubmits.FullName,
                ExtRequestSubmissionDate = extension.ExtRequestSubmissionDate,
                HRAudits = extension.HRAudits.FullName,
                ExtRequestAuditDate = extension.ExtRequestAuditDate,
                ExtNumber = extension.ExtNumber,
                ExtReason = extension.ExtReason,
                ExtRequestStatus = extension.ExtRequestStatus
            };

            return View(model);
        }

        // POST: ExtensionRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Remove extension request from database
            ExtensionRequest extension = db.ExtensionRequests.Find(id);
            db.ExtensionRequests.Remove(extension);
            db.SaveChanges();
            return RedirectToAction("AllIndex");
        }
    }
}
