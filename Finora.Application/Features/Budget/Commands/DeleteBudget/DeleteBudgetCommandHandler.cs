using Finora.Application.Exceptions;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;

namespace Finora.Application.Features.Budget.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository, ICurrentUserService currentUserService)
        {
            _budgetRepository = budgetRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var budget = await _budgetRepository.GetByIdAsync(request.id, userId);

            if (budget == null)
                throw new NotFoundException("Budget not found.");

             await _budgetRepository.DeleteAsync(budget);

            await _budgetRepository.SaveChangesAsync();
        }
    }
}
