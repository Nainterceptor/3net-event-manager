using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _3NET_SyncWorld.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ContributionType> ContributionTypes { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Friends).WithMany();
            modelBuilder.Entity<Event>()
                .HasMany<ApplicationUser>(e => e.InvitedUsers)
                .WithMany(u => u.EventsInvitedTo)
                .Map(c =>
                {
                    c.MapLeftKey("Event_id");
                    c.MapRightKey("User_id");
                    c.ToTable("Invitations");
                });
        }
    }

    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var statuses = new List<Status>
            {
                new Status {Id = 1, Label = "Open"},
                new Status {Id = 2, Label = "Pending"},
                new Status {Id = 3, Label = "Closed"}
            };

            var eventTypes = new List<EventType>
            {
                new EventType {Id = 1, Label = "Party"},
                new EventType {Id = 2, Label = "Lunch"},
                new EventType {Id = 3, Label = "Break"}
            };

            var contributionTypes = new List<ContributionType>
            {
                new ContributionType {Id = 1, Label = "Food"},
                new ContributionType {Id = 2, Label = "Money"},
                new ContributionType {Id = 3, Label = "Soft drink"},
                new ContributionType {Id = 4, Label = "Hard drink"}
            };

            foreach (ContributionType r in contributionTypes)
            {
                context.ContributionTypes.Add(r);
            }

            foreach (Status r in statuses)
            {
                context.Statuses.Add(r);
            }

            foreach (EventType r in eventTypes)
            {
                context.EventTypes.Add(r);
            }

            context.SaveChanges();
        }
    }
}