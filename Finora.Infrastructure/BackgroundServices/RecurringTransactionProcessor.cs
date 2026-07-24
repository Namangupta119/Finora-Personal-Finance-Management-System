using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.BackgroundServices
{
    public class RecurringTransactionProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RecurringTransactionProcessor> _logger;
        //private readonly IRecurringTransactionService _recurringTransactionService;

        public RecurringTransactionProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<RecurringTransactionProcessor> logger)
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
            //_recurringTransactionService = recurringTransactionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Recurring Transaction Processor Started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var recurringTransactionRepository = scope.ServiceProvider.GetRequiredService<IRecurringTransactionRepository>();

                    var recurringTransactionService = scope.ServiceProvider.GetRequiredService<IRecurringTransactionService>();

                    _logger.LogInformation("Checking recurring transactions at {Time}", DateTimeOffset.UtcNow);

                    var dueTransactions = await recurringTransactionRepository.GetDueRecurringTransactionsAsync(DateTimeOffset.UtcNow);

                    _logger.LogInformation("Found {Count} due transactions.", dueTransactions.Count);

                    foreach (var transaction in dueTransactions)
                    {
                        try
                        {
                            await recurringTransactionService.ProcessAsync(transaction, stoppingToken);
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError(ex, "Failed to process recurring transaction.");
                        }
                    }


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Recurring transaction processor failed.");

                    // Later:
                    // _logger.LogError(ex, "Recurring transaction processor failed.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
