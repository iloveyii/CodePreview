using Accounting.Client.Components;
using Accounting.Client.Components.Expense;
using Accounting.Shared;

namespace Accounting.Client.Services
{
    public interface IDataService
    {
        Task<Earning[]> LoadEarnings();
        Task<ICollection<YearlyItem>> LoadCurrentYearEarnings();
        Task DeleteEarning(Guid id);
        Task AddEarning(EarningModel earning);
        Task<Expense[]> LoadExpenses();
        Task<ICollection<YearlyItem>> LoadCurrentYearExpenses();
        Task DeleteExpense(Guid id);
        Task AddExpense(ExpenseModel earning);
        Task<ThreeMonthsData> LoadLast3MonthsEarnings();
        Task<ThreeMonthsData> LoadLast3MonthsExpenses();
    }
}
