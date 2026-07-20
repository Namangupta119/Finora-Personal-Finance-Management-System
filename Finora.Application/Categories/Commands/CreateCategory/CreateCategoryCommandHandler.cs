using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities;
using MediatR;
using System;


namespace Finora.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                IconKey = request.IconKey,
                ColorKey = request.ColorKey,
                UserId = _currentUserService.UserId,
                IsSystem = false,
                IsArchived = false,
                DisplayOrder = 0
            };

            await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveChangesAsync();

            return category.Id;

        }

        
    }
}
