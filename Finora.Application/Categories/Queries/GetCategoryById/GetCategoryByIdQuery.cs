using Finora.Application.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
    }
}
