using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlazorExpenseTracker.Model;
using Dapper;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConfiguration _connectionString;

        public CategoryRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }
        
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var db = DbConnection();
            const string sql = "SELECT * FROM Categories";
            return await db.QueryAsync<Category>(sql,new{});
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            var db = DbConnection();
            const string sql = "SELECT * FROM Categories WHERE Id=@Id";
            return await db.QueryFirstOrDefaultAsync<Category>(sql,new {Id=id});
        }

        public async Task<bool> InsertCategory(Category category)
        {
            var db = DbConnection();
            const string sql = "INSERT INTO Categories (Name) VALUES (@Name)";
            var result= await db.ExecuteAsync(sql, new {category.Name});
            return result>0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var db = DbConnection();
            const string sql = "UPDATE Categories SET Name = @Name WHERE Id=@Id";
            var result = await db.ExecuteAsync(sql, new { category.Id, category.Name });
            return result > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var db = DbConnection();
            const string sql = "DELETE FROM Categories WHERE Id=@Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
