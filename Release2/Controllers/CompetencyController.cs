﻿using Project._1.Models;
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
    public class CompetencyController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Competency
        public ActionResult Index()
        {
            var competency = db.Competencies.ToList();
            var model = new List<CompetencyViewModel>();
            foreach (var item in competency)
            {
                model.Add(new CompetencyViewModel
                {
                    Id = item.CompetencyId,
                    CompetencyName = item.CompetencyName
                });
            }
            return View(model);
        }

        // GET: Competency/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Competency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competency/Create
        [HttpPost]
        public ActionResult Create(CompetencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var competency = new Competency
                {
                    CompetencyName = model.CompetencyName
                };

                db.Competencies.Add(competency);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        // GET: Competency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Competency competency = db.Competencies.Find(id);
            if (competency == null)
            {
                return HttpNotFound();
            }

            CompetencyViewModel model = new CompetencyViewModel
            {
                Id = competency.CompetencyId,
                CompetencyName = competency.CompetencyName
            };

            return View(model);
        }

        // POST: Competency/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CompetencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var competency = db.Competencies.Find(id);
                if (competency != null)
                {
                    competency.CompetencyName = model.CompetencyName;
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

        // GET: Competency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competency competency = db.Competencies.Find(id);
            if (competency == null)
            {
                return HttpNotFound();
            }
            CompetencyViewModel model = new CompetencyViewModel
            {
                Id = competency.CompetencyId,
                CompetencyName = competency.CompetencyName,
            };

            return View(model);
        }

        // POST: Competency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competency competency = db.Competencies.Find(id);
            db.Competencies.Remove(competency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
