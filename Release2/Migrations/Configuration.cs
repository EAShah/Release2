namespace Release2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Project._1.Models;
    using Release2.Models;
    using System;
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

            AddDepartments(context);
        }

            private void AddDepartments(ApplicationDbContext context)
            {
                context.Departments.AddOrUpdate(
                    p => p.DepartmentName,
                     new Department { DepartmentName = "HR" },
                     new Department { DepartmentName = "Marketing" }
                    );
             }
    }
}
