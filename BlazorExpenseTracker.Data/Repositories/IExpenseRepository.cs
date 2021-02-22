using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.Data.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetExpenseDetails(int id);
        Task<bool> InsertExpenseDetails(Expense expense);
        Task<bool> UpdateExpenseDetails(Expense expense);
        Task<bool> DeleteExpenseDetails(int id);
    }
}