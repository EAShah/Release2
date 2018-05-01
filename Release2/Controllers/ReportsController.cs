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

            var totalScores = db.ProgressReviews.Select(t => t.TotalScore).ToList();
            var levels = db.ProbationaryColleagues.Select(l => l.Level);
                //db.ProbationaryColleagues.Where(o => o.AssessedProgressReviews.Select(p => p.TotalScore).ToList() == totalScores).Select(l => l.Level);
            //var levelsOfTScores = db.ProgressReviews.Where(p => p.TotalScore).Select(t=>t.ProbationaryColleague.Level);
            //int count = 0;
            int levelOfTScore = 0;
            var labels = new List<string>();
            var data = new List<int>();
            foreach (var item in totalScores)
            {
                // Get the total score of each level

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