using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetExpenseDetails(int id);
        Task SaveExpenseDetails(Expense category);
        Task DeleteExpenseDetails(int id);
    }
}
