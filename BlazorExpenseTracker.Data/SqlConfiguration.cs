using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker.Data
{
    public class SqlConfiguration
    {
        public string ConnectionString { get; }
        public SqlConfiguration(string connectionString) => ConnectionString = connectionString;
    }
}
