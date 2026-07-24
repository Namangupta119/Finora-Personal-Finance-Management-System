using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;


namespace Finora.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, _currentUserService.UserId);

            if (category == null)
                throw new NotFoundException("Category with this id not found");

            category.Name = request.Name;
            category.Description = request.Description;
            category.IconKey = request.IconKey;
            category.ColorKey = request.ColorKey;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
