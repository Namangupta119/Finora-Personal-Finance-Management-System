using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Services;
using Finora.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.BackgroundServices
{
    public class RecurringTransactionProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        //private readonly IRecurringTransactionService _recurringTransactionService;

        public RecurringTransactionProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            _scopeFactory = serviceScopeFactory;
            //_recurringTransactionService = recurringTransactionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Recurring Transaction Processor Started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var recurringTransactionRepository = scope.ServiceProvider.GetRequiredService<IRecurringTransactionRepository>();

                    var recurringTransactionService = scope.ServiceProvider.GetRequiredService<IRecurringTransactionService>();

                    Console.WriteLine($"Checking recurring transactions at {DateTimeOffset.UtcNow}");

                    var dueTransactions =
                        await recurringTransactionRepository
                            .GetDueRecurringTransactionsAsync(DateTimeOffset.UtcNow);

                    Console.WriteLine($"Found {dueTransactions.Count} due transactions.");

                    foreach (var transaction in dueTransactions)
                    {
                        try
                        {
                            await recurringTransactionService.ProcessAsync(transaction, stoppingToken);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    // Later:
                    // _logger.LogError(ex, "Recurring transaction processor failed.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
