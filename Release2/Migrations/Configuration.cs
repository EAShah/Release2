namespace Release2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Project._1.Models;
    using Release2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  Definition of user roles
            string[] roles = { "HRAssociate", "DepartmentHead", "LineManager", "ProbationaryColleague" };

            string adminEmail = "admin@car.go";
            string adminUserName = "admin@car.go";
            string adminPassword = "Admin1!";

            // Creation of roles
            var roleStore = new CustomRoleStore(context);
            var roleManager = new RoleManager<CustomRole, int>(roleStore);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new CustomRole { Name = role });
                }
            }

            //  Define admin user 
            var userStore = new CustomUserStore(context);
            var userManager = new ApplicationUserManager(userStore);

            // Changing the type of admin user
            var admin = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            //  Create admin user
            if (userManager.FindByName(admin.UserName) == null)
            {
                userManager.Create(admin, adminPassword);
            }

            //  Add admin user to admin role
            // roles[0] is "HRAssociate"
            var user = userManager.FindByName(admin.UserName);
            if (!userManager.IsInRole(user.Id, roles[0]))
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }


            var Colleagues = new List<Colleague>
            {
                new Colleague { UserName = "HRAssociate1", Email ="HRA@gmail.com", FirstName ="HR", LastName ="Associaate", ColleagueType = 0, EmploymentType = 0, ColleagueRegion =0,
                    },

                 new Colleague { UserName = "LineManager1", Email ="LM@gmail.com", FirstName ="Line", LastName ="Manager", ColleagueType = 0, EmploymentType = 0, ColleagueRegion =0,
                    },
            };

            //foreach (var Colleague in Colleagues)
            //{
            //    if (userManager.FindByName(Colleague.UserName) == null)
            //    {
            //        userManager.Create(Colleague, "fac123");
            //    }

            //    var usertemp = userManager.FindByName(Colleague.UserName);
            //    if (!userManager.IsInRole(usertemp.Id, roles[1]))
            //    {
            //        userManager.AddToRole(usertemp.Id, roles[1]);
            //    }
            //}

            var probationaryColleagues = new List<ProbationaryColleague>
            {
                new ProbationaryColleague { UserName = "ProbationaryColleague", Email ="PC@gmail.com", FirstName ="Probationary", LastName ="Colleague1", ColleagueType = 0, EmploymentType = 0, ColleagueRegion =0, ProbationType = 0
                    },

                 new ProbationaryColleague { UserName = "ProbationaryColleague1", Email ="PC1@gmail.com", FirstName ="Probationary", LastName ="Colleague1", ColleagueType = 0, EmploymentType = 0, ColleagueRegion =0, ProbationType= 0
                    },
            };

            //foreach (var ProbationaryColleague in probationaryColleagues)
            //{
            //    if (userManager.FindByName(ProbationaryColleague.UserName) == null)
            //    {
            //        userManager.Create(ProbationaryColleague, "PColleague123");
            //    }

            //    var usertemp = userManager.FindByName(ProbationaryColleague.UserName);
            //    if (!userManager.IsInRole(usertemp.Id, roles[1]))
            //    {
            //        userManager.AddToRole(usertemp.Id, roles[1]);
            //    }
            //}

            AddDepartments(context);
        }

        private void AddDepartments(ApplicationDbContext context)
            {
                context.Departments.AddOrUpdate(
                    p => p.DepartmentName,
                     new Department { DepartmentName = "People's Team" },
                     new Department { DepartmentName = "Marketing" },
                     new Department { DepartmentName = "Qulaity" }
                    );
            }

    }
}
