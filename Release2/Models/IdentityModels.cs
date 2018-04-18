using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project._1.Models;

namespace Release2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Colleague> Colleagues { get; set; }
        public virtual DbSet<Competency> Competencies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<ExtensionRequest> ExtensionRequests { get; set; }
        public virtual DbSet<PerformanceCriterion> PerformanceCriterions { get; set; }
        public virtual DbSet<ProbationaryColleague> ProbationaryColleagues { get; set; }
        public virtual DbSet<ProgressReview> ProgressReviews { get; set; }
        //public virtual DbSet<SelfAssessment> SelfAssessments { get; set; }
        //public virtual DbSet<SelfAssessmentSubmission> SelfAssessmentSubmissions { get; set; }

        // Method declaring navigational properties of the database model.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.CreatedAssignments)
                .WithRequired(e => e.HRAssigns)
                .HasForeignKey(e => e.HRAssignId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.ReceivedAssignment)
                .WithRequired(e => e.LMAssigned)
                .HasForeignKey(e => e.LMAssignId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colleague>() 
                .HasMany(e => e.InspectionAssignment)
                .WithOptional(e => e.LMInspects)
                .HasForeignKey(e => e.LMInspectId);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.SubmittedExtensionRequests)
                .WithRequired(e => e.LMSubmits)
                .HasForeignKey(e => e.LMSubmitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.AuditedExtensionRequests)
                .WithOptional(e => e.HRAudits)
                .HasForeignKey(e => e.HRAuditId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Colleague>()
            //    .HasOptional(e => e.ProbationaryColleague)
            //    .WithRequired(e => e.Colleague);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.ApprovedProgressReviews)
                .WithOptional(e => e.DHApproval)
                .HasForeignKey(e => e.PRDHApprovesId);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.EvaluatedProgressReviews)
                .WithOptional(e => e.HREvaluation)
                .HasForeignKey(e => e.HREvaluatesId);

            modelBuilder.Entity<Colleague>()
                .HasMany(e => e.CreatedProgressReviews)
                .WithRequired(e => e.LMCreates)
                .HasForeignKey(e => e.LMId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Colleague>()
            //    .HasMany(e => e.ApprovedSelfAssessments)
            //    .WithOptional(e => e.DHApprovals)
            //    .HasForeignKey(e => e.DHApprovesID);

            //modelBuilder.Entity<Colleague>()
            //    .HasMany(e => e.ReviewedSelfAssessments)
            //    .WithOptional(e => e.HRReviews)
            //    .HasForeignKey(e => e.HRReviewsID);

            modelBuilder.Entity<Competency>()
                .HasMany(e => e.PerformanceCriterions)
                .WithRequired(e => e.Competency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Colleagues)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
             .HasMany(e => e.Assignments)
             .WithRequired(e => e.Department)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProbationaryColleague>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.ProbationaryColleague)
                .HasForeignKey(e => e.PCId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProbationaryColleague>()
                .HasMany(e => e.ExtensionRequests)
                .WithRequired(e => e.ExtendedPC)
                .HasForeignKey(e => e.ExtendedPCId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProbationaryColleague>()
                .HasMany(e => e.ProgressReviews)
                .WithRequired(e => e.ProbationaryColleague)
                .HasForeignKey(e => e.PCId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProbationaryColleague>()
                .HasMany(e => e.AssessedProgressReviews)
                .WithOptional(e => e.CreationPC)
                .HasForeignKey(e => e.CreationPCId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgressReview>()
                .HasMany(e => e.PerformanceCriterions)
                .WithRequired(e => e.ProgressReview)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ProgressReview>()
            //    .HasOptional(e => e.SelfAssessment)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(false);

        }
    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}