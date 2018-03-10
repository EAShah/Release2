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
    public class DepartmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Department
        public ActionResult Index()
        {
            var departments = db.Departments.ToList();
            var model = new List<DepartmentViewModel>();
            foreach (var item in departments)
            {
                model.Add(new DepartmentViewModel
                {
                    Id = item.DepartmentId,
                    DepartmentName = item.DepartmentName
                });
            }
            return View(model);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    DepartmentName = model.DepartmentName
                };

                db.Departments.Add(department);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel model = new DepartmentViewModel
            {
                Id = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };

            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = db.Departments.Find(id);
                if (department != null)
                {
                    department.DepartmentName = model.DepartmentName;
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

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            DepartmentViewModel model = new DepartmentViewModel
            {
                Id = department.DepartmentId,
                DepartmentName = department.DepartmentName,
            };

            return View(model);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
