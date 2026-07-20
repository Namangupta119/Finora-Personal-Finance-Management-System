using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Queries.GetCategories
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string IconKey { get; set; } = string.Empty;

        public string ColorKey { get; set; } = string.Empty;
        public bool IsSystem { get; set; }
    }
}
