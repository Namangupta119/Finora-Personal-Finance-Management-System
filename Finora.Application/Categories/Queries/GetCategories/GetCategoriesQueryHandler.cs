using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;


namespace Finora.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(_currentUserService.UserId);

            var result = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IconKey = c.IconKey,
                ColorKey = c.ColorKey,
                IsSystem = c.IsSystem,
            }).ToList();

            return result;
        }
    }
}
