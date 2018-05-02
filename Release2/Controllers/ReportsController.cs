using Release2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Release2.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            // This chart displays on
            // x-axes: Department names that contain one or more faculties
            // y-axes: Number of faculties in each department

            //var probationaryColleagues = db.ProbationaryColleagues.ToList();
            //var progressReviews = db.ProgressReviews;

            var probationaryColleagues = db.ProbationaryColleagues;
            var levels = db.ProbationaryColleagues.Select(l => l.Level).ToList();

            int count = 0;
            var labels = new List<string>();
            var data = new List<int>();
            foreach (var item in levels)
            {
                count = probationaryColleagues.Count(p => p.Level == item);

                if (count != 0)
                {
                    labels.Add(item.ToString());
                    data.Add(count);
                }


                // Get the total scores of all levels of one probationary colleague

                //count = progressReviews.Count(i => i.ProbationaryColleague.AssessedProgressReviews.Select(l => l.TotalScore) == item.AssessedProgressReviews.Select(l => l.TotalScore));
                //if (count != 0)
                //{
                //    labels.Add(item.Level.ToString());
                //    data.Add(count);
                //}

                //// Find the number of faculties in the current department
                //count = faculties.Count(m => m.Department.Id == item.Id);
                //if (count != 0)
                //{
                //    labels.Add(item.Name);
                //    data.Add(count);
                //}
            }

            // Convert labels and data from lists to arrays and save them in ViewBag
            ViewBag.Labels = labels.ToArray();
            ViewBag.Data = data.ToArray();

            return View();
        }
    }
}