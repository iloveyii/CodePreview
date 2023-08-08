using Accounting.Server.Storage;
using Accounting.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Server
{
    public static class SampleData
    {
        public static void AddEarningsRepository(this IServiceCollection services)
        {
            var today = DateTime.Today;
            var lastMonth = DateTime.Today.AddMonths(-1);
            var previousMonth = DateTime.Today.AddMonths(-2);

            var earningsRepository = new MemoryRepository<Earning>();

            earningsRepository.Add(new Earning { Date = new DateTime(previousMonth.Year, previousMonth.Month, 25), Amount = 1230, Category = EarningCategory.Freelancing, Subject = "Freelancing client A", UserName = "admin" });
            earningsRepository.Add(new Earning { Date = new DateTime(previousMonth.Year, previousMonth.Month, 15), Amount = 2146, Category = EarningCategory.Flipping, Subject = "Flipping client A", UserName = "user" });
            earningsRepository.Add(new Earning { Date = new DateTime(previousMonth.Year, previousMonth.Month, 5), Amount = 5300, Category = EarningCategory.Coaching, Subject = "Coaching client A", UserName = "admin" });
            earningsRepository.Add(new Earning { Date = new DateTime(lastMonth.Year, lastMonth.Month, 5), Amount = 1200, Category = EarningCategory.CapitalGain, Subject = "Capital client A", UserName = "admin" });
            earningsRepository.Add(new Earning { Date = new DateTime(lastMonth.Year, lastMonth.Month, 17), Amount = 2300, Category = EarningCategory.Freelancing, Subject = "Freelancing client A", UserName = "user" });
            earningsRepository.Add(new Earning { Date = new DateTime(lastMonth.Year, lastMonth.Month, 12), Amount = 3000, Category = EarningCategory.Gift, Subject = "Old TV client A", UserName = "admin" });
            earningsRepository.Add(new Earning { Date = new DateTime(today.Year, today.Month, 14), Amount = 1300, Category = EarningCategory.Freelancing, Subject = "Tech client A", UserName = "user" });
            earningsRepository.Add(new Earning { Date = new DateTime(today.Year, today.Month, 15), Amount = 1300, Category = EarningCategory.Coaching, Subject = "Support client A", UserName = "user" });
            earningsRepository.Add(new Earning { Date = new DateTime(today.Year, today.Month, 16), Amount = 11000, Category = EarningCategory.Salary, Subject = "Last client A", UserName = "admin" });

            services.AddSingleton<IRepository<Earning>>(earningsRepository);
        }

        public static void AddExpensesRepository(this IServiceCollection services)
        {
            var today = DateTime.Today;
            var lastMonth = DateTime.Today.AddMonths(-1);
            var previousMonth = DateTime.Today.AddMonths(-2);

            var expensesRepository = new MemoryRepository<Expense>();

            expensesRepository.Add(new Expense { Date = new DateTime(previousMonth.Year, previousMonth.Month, 25), Amount = 214, Category = ExpenseCategory.Clothing, Subject = "Clothing client A", UserName = "user" });
            expensesRepository.Add(new Expense { Date = new DateTime(previousMonth.Year, previousMonth.Month, 15), Amount = 200, Category = ExpenseCategory.Savings, Subject = "Savings client A", UserName = "admin" });
            expensesRepository.Add(new Expense { Date = new DateTime(previousMonth.Year, previousMonth.Month, 5), Amount = 700, Category = ExpenseCategory.Insurance, Subject = "Insurance client A", UserName = "admin" });
            expensesRepository.Add(new Expense { Date = new DateTime(lastMonth.Year, lastMonth.Month, 5), Amount = 120, Category = ExpenseCategory.Donations, Subject = "Donations client A", UserName = "admin" });
            expensesRepository.Add(new Expense { Date = new DateTime(lastMonth.Year, lastMonth.Month, 17), Amount = 150, Category = ExpenseCategory.Housing, Subject = "Freelancing client A", UserName = "user" });
            expensesRepository.Add(new Expense { Date = new DateTime(lastMonth.Year, lastMonth.Month, 12), Amount = 300, Category = ExpenseCategory.Transportation, Subject = "Old TV client A", UserName = "admin" });
            expensesRepository.Add(new Expense { Date = new DateTime(today.Year, today.Month, 14), Amount = 130, Category = ExpenseCategory.Debt, Subject = "Tech client A", UserName = "user" });
            expensesRepository.Add(new Expense { Date = new DateTime(today.Year, today.Month, 15), Amount = 100, Category = ExpenseCategory.Insurance, Subject = "Support client A", UserName = "user" });
            expensesRepository.Add(new Expense { Date = new DateTime(today.Year, today.Month, 16), Amount = 110, Category = ExpenseCategory.Food, Subject = "Last client A", UserName = "admin" });

            services.AddSingleton<IRepository<Expense>>(expensesRepository);
        }

    }
}
