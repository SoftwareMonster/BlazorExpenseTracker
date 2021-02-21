using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;
using Dapper;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly SqlConfiguration _connectionString;

        public ExpenseRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var db = DbConnection();
            const string sql = @"SELECT e.Id,Amount,CategoryId,ExpenseType,TransactionDate,c.Id,c.Name
                                    FROM Expenses e 
                                    INNER JOIN Categories c
                                        ON e.CategoryId= c.Id";
            var result = await db.QueryAsync<Expense, Category, Expense>(sql,
                (expense, category) =>
                {
                    expense.Category = category;
                    return expense;
                }, new { }, splitOn: "Id");
            return result;
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            var db = DbConnection();
            const string sql = @"SELECT * FROM Expenses WHERE Id=@id";
            var result = await db.QueryFirstOrDefaultAsync<Expense>(sql, new {Id = id});
            return result;
        }

        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            var db = DbConnection();
            const string sql =
                "INSERT INTO Expenses (Amount,CategoryId,ExpenseType,TransactionDate) VALUES (@Amount,@CategoryId,@ExpenseType,@TransactionDate)";
            var result = await db.ExecuteAsync(sql,
                new {expense.Amount, expense.CategoryId, expense.ExpenseType, expense.TransactionDate});
            return result > 0;
        }

        public async Task<bool> UpdateExpenseDetails(Expense expense)
        {
            var db = DbConnection();
            const string sql = @"UPDATE Expenses 
                                    SET Amount = @Amount,CategoryId=@CategoryId,ExpenseType=@ExpenseType,TransactionDate=@TransactionDate  
                                 WHERE Id=@Id";
            var result = await db.ExecuteAsync(sql,
                new {expense.Id, expense.Amount, expense.CategoryId, expense.ExpenseType, expense.TransactionDate});
            return result > 0;
        }

        public async Task<bool> DeleteExpenseDetails(int id)
        {
            var db = DbConnection();
            const string sql = "DELETE FROM Expenses WHERE Id=@Id";
            var result = await db.ExecuteAsync(sql, new {Id = id});
            return result > 0;
        }

        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }
    }
}