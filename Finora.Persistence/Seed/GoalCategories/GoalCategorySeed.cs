using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Seed.GoalCategories
{
    public static class GoalCategorySeed
    {
        public static IEnumerable<GoalCategory> GetGoalCategories()
        {
            return
            [
                new GoalCategory
                {
                    Id = GoalCategoryIds.EmergencyFund,
                    Name = "Emergency Fund",
                    Description = "Save money for unexpected emergencies.",
                    Icon = "shield",
                    DisplayOrder = 10,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Home,
                    Name = "Home",
                    Description = "Save towards buying or renovating a home.",
                    Icon = "home",
                    DisplayOrder = 20,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Vehicle,
                    Name = "Vehicle",
                    Description = "Save for purchasing a vehicle.",
                    Icon = "directions_car",
                    DisplayOrder = 30,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Education,
                    Name = "Education",
                    Description = "Save for education and professional growth.",
                    Icon = "school",
                    DisplayOrder = 40,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Travel,
                    Name = "Travel",
                    Description = "Save for vacations and travel plans.",
                    Icon = "flight",
                    DisplayOrder = 50,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Investment,
                    Name = "Investment",
                    Description = "Build long-term investment wealth.",
                    Icon = "trending_up",
                    DisplayOrder = 60,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Retirement,
                    Name = "Retirement",
                    Description = "Save for a financially secure retirement.",
                    Icon = "savings",
                    DisplayOrder = 70,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Wedding,
                    Name = "Wedding",
                    Description = "Save for wedding and related expenses.",
                    Icon = "favorite",
                    DisplayOrder = 80,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Gadget,
                    Name = "Gadget",
                    Description = "Save for electronic gadgets and devices.",
                    Icon = "laptop",
                    DisplayOrder = 90,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Business,
                    Name = "Business",
                    Description = "Save to start or expand a business.",
                    Icon = "business_center",
                    DisplayOrder = 100,
                    IsActive = true
                },

                new GoalCategory
                {
                    Id = GoalCategoryIds.Other,
                    Name = "Other",
                    Description = "Other financial goals.",
                    Icon = "category",
                    DisplayOrder = 110,
                    IsActive = true
                }
            ];
        }
    }
}
