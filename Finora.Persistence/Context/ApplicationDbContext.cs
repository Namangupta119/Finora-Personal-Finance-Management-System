using Finora.Domain.Common;
using Finora.Domain.Entities;
using Finora.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finora.Persistence.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<UserSetting> UserSettings => Set<UserSetting>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Income> Incomes => Set<Income>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Goal> Goals => Set<Goal>();
        public DbSet<GoalCategory> GoalCategories => Set<GoalCategory>();
        public DbSet<GoalContribution> GoalContributions => Set<GoalContribution>();
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTimeOffset.UtcNow;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = DateTimeOffset.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
