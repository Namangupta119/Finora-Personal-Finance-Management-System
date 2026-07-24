using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(ICurrentUserService currentUserService, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, _currentUserService.UserId);

            if (category == null)
                throw new NotFoundException("Category not found");

            category.IsArchived = true;

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
