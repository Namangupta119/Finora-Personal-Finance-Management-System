using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Features.Dashboard.GetRecentTransactions
{
    public class GetRecentTransactionsQuery : IRequest<IReadOnlyList<RecentTransactionDto>>;
}
