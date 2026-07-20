using Finora.Application.Categories.Queries.GetCategories;
using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;

        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, _currentUserService.UserId);

            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                IconKey = category.IconKey,
                ColorKey = category.ColorKey,
                IsSystem = category.IsSystem,
            };
        }
    }
}
