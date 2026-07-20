using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Seed.Categories
{
    public static class CategorySeed
    {
        public static IEnumerable<Category> GetCategories()
        {
            return
            [
                new Category
                {
                    Id = CategoryIds.Food,
                    Name = "Food",
                    Description = "Food and dining expenses.",
                    IconKey = "food",
                    ColorKey = "green",
                    DisplayOrder = 10,
                    IsSystem = true,
                    IsArchived = false
                },

                new Category
            {
                Id = CategoryIds.Travel,
                Name = "Travel",
                Description = "Travel and transportation expenses.",
                IconKey = "travel",
                ColorKey = "blue",
                DisplayOrder = 20,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Shopping,
                Name = "Shopping",
                Description = "Shopping expenses.",
                IconKey = "shopping",
                ColorKey = "purple",
                DisplayOrder = 30,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Bills,
                Name = "Bills",
                Description = "Utility and recurring bills.",
                IconKey = "receipt",
                ColorKey = "red",
                DisplayOrder = 40,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Entertainment,
                Name = "Entertainment",
                Description = "Movies, games and entertainment.",
                IconKey = "movie",
                ColorKey = "orange",
                DisplayOrder = 50,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Medical,
                Name = "Medical",
                Description = "Healthcare and medical expenses.",
                IconKey = "medical",
                ColorKey = "teal",
                DisplayOrder = 60,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Education,
                Name = "Education",
                Description = "Education and learning expenses.",
                IconKey = "school",
                ColorKey = "indigo",
                DisplayOrder = 70,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Salary,
                Name = "Salary",
                Description = "Salary and income.",
                IconKey = "wallet",
                ColorKey = "emerald",
                DisplayOrder = 80,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Investment,
                Name = "Investment",
                Description = "Investment related transactions.",
                IconKey = "chart",
                ColorKey = "cyan",
                DisplayOrder = 90,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Gift,
                Name = "Gift",
                Description = "Gift expenses.",
                IconKey = "gift",
                ColorKey = "pink",
                DisplayOrder = 100,
                IsSystem = true,
                IsArchived = false
            },

            new Category
            {
                Id = CategoryIds.Other,
                Name = "Other",
                Description = "Other uncategorized expenses.",
                IconKey = "category",
                ColorKey = "gray",
                DisplayOrder = 110,
                IsSystem = true,
                IsArchived = false
            }
            ];
        }
    }
}
