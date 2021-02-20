using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker.Model
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
