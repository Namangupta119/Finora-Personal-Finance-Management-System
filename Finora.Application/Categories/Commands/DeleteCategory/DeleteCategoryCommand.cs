using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
