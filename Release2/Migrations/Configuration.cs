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


            var departments = new List<Department>
            {
                     new Department { DepartmentName = "People's Team" },
                     new Department { DepartmentName = "Marketing" },
                     new Department { DepartmentName = "Quality" }
            };

            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.DepartmentName, s));
            context.SaveChanges();

            var competencies = new List<Competency>
            {
                     new Competency { CompetencyName = "Social Responsibility" },
                     new Competency { CompetencyName = "Knowledge" },
                     new Competency { CompetencyName = "Management" }
            };

            competencies.ForEach(c => context.Competencies.AddOrUpdate(p => p.CompetencyName, c));
            context.SaveChanges();

            //var assignments = new List<Assignment>
            //{
            //         new Assignment { HRAssignId = 2, LMAssignId = 6, PCId = 8, AssignmentStatus = Assignment.AssignStatus.Approved, AssignmentDate = null, AssignmentInspectionDate = null},
            //         new Assignment { HRAssignId = 2, LMAssignId = 7, PCId = 9, AssignmentStatus = Assignment.AssignStatus.Pending, AssignmentDate = null, AssignmentInspectionDate = null},
            //};

            //assignments.ForEach(c => context.Assignments.AddOrUpdate(p => p.AssignmentId.ToString(), c));
            //context.SaveChanges();
            var probationaryColleagues = new List<ProbationaryColleague>
            {
                new ProbationaryColleague { UserName = "PC", Email ="PC@gmail.com", FirstName ="Probationary", LastName ="Colleague1", ColleagueType = ColleagueType.ProbationaryColleague, EmploymentType = EmploymentType.FullTimer, ColleagueRegion = ColleagueRegion.Jeddah, ProbationType = ProbationaryColleague.ProbationTypes.Mandatory, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", CityOfProbation = ProbationaryColleague.Cities.Jeddah, Level = ProbationaryColleague.Levels.First, JoinDate = null, LeaveDate = null, Nationality = " ", Gender = Gender.Female,DepartmentId = 1,
                    },

                 new ProbationaryColleague { UserName = "PC1", Email ="PC1@gmail.com", FirstName ="Probationary", LastName ="Colleague2", ColleagueType = ColleagueType.ProbationaryColleague, EmploymentType = EmploymentType.FullTimer, ColleagueRegion = ColleagueRegion.Makkah, ProbationType= ProbationaryColleague.ProbationTypes.Mandatory, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", CityOfProbation = ProbationaryColleague.Cities.Jeddah, Level = ProbationaryColleague.Levels.Third, JoinDate = null, LeaveDate = null, Nationality = " ", Gender = Gender.Male,DepartmentId = 2,
                    },
            };

            foreach (var ProbationaryColleague in probationaryColleagues)
            {
                if (userManager.FindByName(ProbationaryColleague.UserName) == null)
                {
                    userManager.Create(ProbationaryColleague, "qwe123");
                }

                var usertemp = userManager.FindByName(ProbationaryColleague.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[3]))
                {
                    userManager.AddToRole(usertemp.Id, roles[3]);
                }
            }

            var LineManagers = new List<Colleague>
            {
                new Colleague { UserName = "LM1", Email ="LM1@gmail.com", FirstName ="Line", LastName ="Manager1", ColleagueType = ColleagueType.LineManager, EmploymentType = EmploymentType.FullTimer, ColleagueRegion =ColleagueRegion.Jeddah, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ",Nationality = " ", Gender = Gender.Male,DepartmentId = 1,
                    },

                new Colleague { UserName = "LM2", Email ="LM2@gmail.com", FirstName ="Line", LastName ="Manager2", ColleagueType = ColleagueType.LineManager, EmploymentType = EmploymentType.FullTimer, ColleagueRegion =ColleagueRegion.Makkah, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ",Nationality = " ", Gender = Gender.Female,DepartmentId = 2,
                    },
            };

            foreach (var Colleague in LineManagers)
            {
                if (userManager.FindByName(Colleague.UserName) == null)
                {
                    userManager.Create(Colleague, "qwe123");
                }

                var usertemp = userManager.FindByName(Colleague.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[2]))
                {
                    userManager.AddToRole(usertemp.Id, roles[2]);
                }
            }

            var extensions = new List<ExtensionRequest>
            {
                     new ExtensionRequest { ExtReason = "Colleague performance is unsatisfactory in this month.", ExtNumber = ProbationaryColleague.Levels.Five, ExtRequestStatus= ExtensionRequest.RequestStatus.Pending, ExtendedPCId = 2, LMSubmitId = 2, ExtRequestAuditDate = null, ExtRequestSubmissionDate = null},
                     new ExtensionRequest { ExtReason = "Colleague performance is unsatisfactory in this month.", ExtNumber = ProbationaryColleague.Levels.Third, ExtRequestStatus= ExtensionRequest.RequestStatus.Approved, ExtendedPCId = 2, LMSubmitId = 2, ExtRequestAuditDate = null, ExtRequestSubmissionDate = null},
            };

            extensions.ForEach(c => context.ExtensionRequests.AddOrUpdate(p => p.ExtReason, c));
            context.SaveChanges();

            var HRAssociates = new List<Colleague>
            {
                new Colleague { UserName = "HRA1", Email ="HRA@gmail.com", FirstName ="HR", LastName ="Associate1", ColleagueType = ColleagueType.HRAssociate, EmploymentType = EmploymentType.FullTimer, ColleagueRegion = ColleagueRegion.Jeddah, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", Nationality = " ", Gender = Gender.Male, DepartmentId = 1,
                },

                new Colleague { UserName = "HRA2", Email ="HRA2@gmail.com", FirstName ="HR", LastName ="Associate2", ColleagueType = ColleagueType.HRAssociate, EmploymentType = EmploymentType.FullTimer, ColleagueRegion = ColleagueRegion.Makkah,DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", Nationality = " ", Gender = Gender.Female,DepartmentId = 1,
                },
            };

            foreach (var Colleague in HRAssociates)
            {
                if (userManager.FindByName(Colleague.UserName) == null)
                {
                    userManager.Create(Colleague, "qwe123");
                }

                var usertemp = userManager.FindByName(Colleague.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[0]))
                {
                    userManager.AddToRole(usertemp.Id, roles[0]);
                }
            }

            var DepartmentHeads = new List<Colleague>
            {
                new Colleague { UserName = "DH1", Email ="DH1@gmail.com", FirstName ="Department", LastName ="Head1", ColleagueType = ColleagueType.DepartmentHead, EmploymentType = EmploymentType.FullTimer, ColleagueRegion = ColleagueRegion.Jeddah, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", Nationality = " ", Gender = Gender.Female, DepartmentId = 1,
                    },

                new Colleague { UserName = "DH2", Email ="DH2@gmail.com", FirstName ="Department", LastName ="Head2", ColleagueType = ColleagueType.DepartmentHead, EmploymentType = EmploymentType.FullTimer, ColleagueRegion =ColleagueRegion.Makkah, DOB = null, City = " ", Country = " ", HomeNumber = " ", PhoneNumber = " ", Position = " ", Nationality = " ", Gender = Gender.Male,DepartmentId = 2,
                    },
            };

            foreach (var Colleague in DepartmentHeads)
            {
                if (userManager.FindByName(Colleague.UserName) == null)
                {
                    userManager.Create(Colleague, "qwe123");
                }

                var usertemp = userManager.FindByName(Colleague.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[1]))
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
                }
            }

            

            
        }
    }
}
