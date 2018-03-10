namespace Release2.Migrations
{
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
            context.Departments.AddOrUpdate(
                p => p.DepartmentName,
                 new Department { DepartmentName = "HR" },
                 new Department { DepartmentName = "Marketing" }
                );
        }
    }
}
