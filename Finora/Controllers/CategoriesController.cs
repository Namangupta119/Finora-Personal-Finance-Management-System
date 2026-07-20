using Finora.Application.Categories.Commands.CreateCategory;
using Finora.Application.Categories.Commands.DeleteCategory;
using Finora.Application.Categories.Commands.UpdateCategory;
using Finora.Application.Categories.Queries.GetCategories;
using Finora.Application.Categories.Queries.GetCategoryById;
using Finora.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Finora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery
            {
                Id = id
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);

            return Ok(categoryId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCategory(Guid id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id and request id do not match.");

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand
            {
                Id = id
            });

            return NoContent();
                
        }
    }
}
