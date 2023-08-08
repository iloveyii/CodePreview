using Accounting.Client.Authentication;
using Accounting.Client.Components;
using Accounting.Client.Components.Expense;
using Accounting.Client.Pages;
using Accounting.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Net.Http.Json;


namespace Accounting.Client.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient httpClient;
        private readonly int _currentYear = DateTime.Today.Year;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        public DataService(HttpClient httpClient, AuthenticationStateProvider authStateProvider) 
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authStateProvider;
        }

        private async Task SetAuthHeader()
        {
            try
            {
                var customAuthStateService = (CustomAuthenticationStateProvider)authenticationStateProvider;
                var token = await customAuthStateService.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                }
            }
            catch { }
        }

        public async Task<Earning[]> LoadEarnings()
        {
            await SetAuthHeader();
            var data = await httpClient.GetFromJsonAsync<Earning[]>("api/Earnings");
            return data;
        }
        public async Task<ICollection<YearlyItem>> LoadCurrentYearEarnings()
        {
            await SetAuthHeader();
            var data = await httpClient.GetFromJsonAsync<Earning[]>("api/Earnings");
            var result = data.Where(earning => earning.Date >= new DateTime(_currentYear, 1, 1)
                && earning.Date <= new DateTime(_currentYear, 12, 31))
                .GroupBy(earning => earning.Date.Month)
                .OrderBy(earning => earning.Key)
                .Select(earning => new YearlyItem
                {
                    Month = GetMonthAsText(earning.Key, _currentYear),
                    Amount = earning.Sum(item => item.Amount)
                })
                .ToList();

            return result;
        }

        public async Task DeleteEarning(Guid id)
        {
            await SetAuthHeader();
            await httpClient.DeleteAsync($"api/Earnings/{id}");
        }

        public async Task AddEarning(EarningModel earning)
        {
            await SetAuthHeader();
            await httpClient.PostAsJsonAsync<EarningModel>("api/Earnings", earning);
        }

        public async Task<Expense[]> LoadExpenses()
        {
            await SetAuthHeader();
            var data = await httpClient.GetFromJsonAsync<Expense[]>("api/Expenses");
            return data;
        }

        public async Task<ICollection<YearlyItem>> LoadCurrentYearExpenses()
        {
            await SetAuthHeader();
            var data = await httpClient.GetFromJsonAsync<Expense[]>("api/Expenses");
            return data.Where(expense => expense.Date >= new DateTime(_currentYear, 1, 1)
                && expense.Date <= new DateTime(_currentYear, 12, 31))
                .GroupBy(expense => expense.Date.Month)
                .OrderBy(expense => expense.Key)
                .Select(expense => new YearlyItem
                {
                    Month = GetMonthAsText(expense.Key, _currentYear),
                    Amount = expense.Sum(item => item.Amount)
                })
                .ToList();
        }
        public async Task DeleteExpense(Guid id)
        {
            await SetAuthHeader();
            await httpClient.DeleteAsync($"api/Expenses/{id}");
        }
        public async Task AddExpense(ExpenseModel expense)
        {
            await SetAuthHeader();
            await httpClient.PostAsJsonAsync<ExpenseModel>("api/Expenses", expense);
        }

        public async Task<ThreeMonthsData> LoadLast3MonthsEarnings()
        {
            var currentMonth = DateTime.Today.Month;
            var lastMonth = DateTime.Today.AddMonths(-1);
            var previousMonth = DateTime.Today.AddMonths(-2);

            var result =  new ThreeMonthsData
            {
                CurrentMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(currentMonth, _currentYear),
                    Label = GetMonthAsText(currentMonth, _currentYear),
                },
                LastMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(lastMonth.Month, _currentYear),
                    Label = GetMonthAsText(lastMonth.Month, _currentYear),
                },
                PreviousMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(previousMonth.Month, _currentYear),
                    Label = GetMonthAsText(previousMonth.Month, _currentYear),
                }
            };

            return result;
        }

        public async Task<ICollection<MonthlyItem>> GetMonthlyEarnings(int month, int year)
        {
            var data = await httpClient.GetFromJsonAsync<Earning[]>("api/Earnings");
            return data.Where(earning => earning.Date >= new DateTime(year, month, 1)
                && earning.Date <= new DateTime(year, month, LastDayOfMonth(month, year)))
                .GroupBy(earning => earning.Category)
                .Select(earning => new MonthlyItem
                {
                    Amount = earning.Sum(item => item.Amount),
                    Category = earning.Key.ToString()
                })
                .ToList();
        }

        public async Task<ThreeMonthsData> LoadLast3MonthsExpenses()
        {
            var currentMonth = DateTime.Today.Month;
            var lastMonth = DateTime.Today.AddMonths(-1);
            var previousMonth = DateTime.Today.AddMonths(-2);

            return new ThreeMonthsData
            {
                CurrentMonth = new MonthlyData
                {
                    Data = await GetMonthlyExpenses(currentMonth, _currentYear),
                    Label = GetMonthAsText(currentMonth, _currentYear),
                },
                LastMonth = new MonthlyData
                {
                    Data = await GetMonthlyExpenses(lastMonth.Month, _currentYear),
                    Label = GetMonthAsText(lastMonth.Month, _currentYear),
                },
                PreviousMonth = new MonthlyData
                {
                    Data = await GetMonthlyExpenses(previousMonth.Month, _currentYear),
                    Label = GetMonthAsText(previousMonth.Month, _currentYear),
                }
            };
        }

        private async Task<ICollection<MonthlyItem>> GetMonthlyExpenses(int month, int year)
        {
            var data = await httpClient.GetFromJsonAsync<Expense[]>("api/Expenses");
            return data.Where(expense => expense.Date >= new DateTime(year, month, 1)
            && expense.Date <= new DateTime(year, month, LastDayOfMonth(month, year)))
            .GroupBy(expense => expense.Category)
            .Select(expense => new MonthlyItem
            {
                Amount = expense.Sum(item => item.Amount),  
                Category = expense.Key.ToString()
            })
            .ToList();
        }


        private static int LastDayOfMonth(int month, int year)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static string GetMonthAsText(int month, int year)
        {
            return month switch 
            {
                1 => $"January {year}",
                2 => $"February {year}",
                3 => $"March {year}",
                4 => $"April {year}",
                5 => $"May {year}",
                6 => $"June {year}",
                7 => $"July {year}",
                8 => $"August {year}",
                9 => $"September {year}",
                10 => $"October {year}",
                11 => $"November {year}",
                12 => $"December {year}",
            };
        }
    }
}
