using System;
using System.Collections.Generic;
using System.Net;
using MediatR;

namespace Finora.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
    {
    }
}
