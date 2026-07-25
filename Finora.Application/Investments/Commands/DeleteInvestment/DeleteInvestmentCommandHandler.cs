using Finora.Application.Exceptions;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Investments.Commands.DeleteInvestment
{
    public class DeleteInvestmentCommandHandler : IRequestHandler<DeleteInvestmentCommand>
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvestmentCommandHandler(IInvestmentRepository investmentRepository,ICurrentUserService currentUserService,IUnitOfWork unitOfWork)
        {
            _investmentRepository = investmentRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteInvestmentCommand request,CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var investment = await _investmentRepository.GetByIdAsync(
                request.InvestmentId,
                userId,
                cancellationToken);

            if (investment is null)
            {
                throw new NotFoundException("Investment not found.");
            }

            await _investmentRepository.DeleteAsync(investment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
