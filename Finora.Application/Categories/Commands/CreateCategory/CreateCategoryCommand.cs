using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string IconKey { get; set; } = string.Empty;
        public string ColorKey { get; set; } = string.Empty;
    }
}
