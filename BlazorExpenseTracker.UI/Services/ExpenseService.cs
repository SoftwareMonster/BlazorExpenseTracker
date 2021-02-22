using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;

namespace BlazorExpenseTracker.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient _httpClient;

        public ExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Expense>>(
                await _httpClient.GetStreamAsync("api/expenses"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Expense>(
                await _httpClient.GetStreamAsync($"api/expenses/{id}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task SaveExpenseDetails(Expense expense)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");
            if (expense.Id == 0)
            {
                var a = await _httpClient.PostAsync("api/expenses", categoryJson);
            }
            else
            {
                await _httpClient.PutAsync("api/expenses", categoryJson);
            }
        }

        public async Task DeleteExpenseDetails(int id)
        {
            await _httpClient.DeleteAsync($"api/expenses/{id}");
        }
    }
}