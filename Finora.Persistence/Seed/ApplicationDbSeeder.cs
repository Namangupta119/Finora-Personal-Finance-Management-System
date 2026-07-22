using Finora.Persistence.Context;
using Finora.Persistence.Seed.Categories;
using Finora.Persistence.Seed.GoalCategories;
using Microsoft.EntityFrameworkCore;


namespace Finora.Persistence.Seed
{
    public class ApplicationDbSeeder
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedCategoriesAsync();
            await SeedGoalCategoriesAsync();
        }

        private async Task SeedCategoriesAsync()
        {
            if (await _context.Categories.AnyAsync())
                return;

            var categories = CategorySeed.GetCategories();

            await _context.Categories.AddRangeAsync(categories);

            await _context.SaveChangesAsync();
        }

        private async Task SeedGoalCategoriesAsync()
        {
            if (await _context.GoalCategories.AnyAsync())
                return;

            var goalCategories = GoalCategorySeed.GetGoalCategories();

            await _context.GoalCategories.AddRangeAsync(goalCategories);

            await _context.SaveChangesAsync();
        }
    }
}
