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
        public ActionResult Index()
        {
            var request = db.ExtensionRequests.ToList();
            var model = new List<ExtensionRequestViewModel>();
            foreach (var item in request)
            {
                model.Add(new ExtensionRequestViewModel
                {
                    Id = item.ExtRequestId,
                    ExtNumber = item.ExtNumber,
                    //ExtRequestStatus = item.ExtRequestStatus,
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
            ViewBag.ColleagueId = new SelectList(list, "Id", "FullName");
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
                    //ExtensionSubmissions = db.ProbationaryColleagues.Select(p =>p.FirstName)
                };

                db.ExtensionRequests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        // GET: ExtensionRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExtensionRequest/Edit/5
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

        // GET: ExtensionRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExtensionRequest/Delete/5
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
