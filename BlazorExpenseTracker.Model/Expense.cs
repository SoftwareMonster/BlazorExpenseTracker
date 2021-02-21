using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BlazorExpenseTracker.Model.Validations;

namespace BlazorExpenseTracker.Model
{
    public class Expense //: IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage = "Amount needs to be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ExpenseType ExpenseType { get; set; }
        [Required]
        [ExpenseTransactionDateValidator(DaysInTheFuture = 30)]
        public DateTime TransactionDate { get; set; }

        public event Action OnSelectedExpenseChanged;

        private void NotifySelectedExpenseChanged()
        {
            OnSelectedExpenseChanged.Invoke();
        }

        public void SelectedExpenseChanged(Expense expense)
        {
            Id = expense.Id;
            TransactionDate = expense.TransactionDate;
            Amount = expense.Amount;
            ExpenseType = expense.ExpenseType;
            CategoryId = expense.CategoryId;
            NotifySelectedExpenseChanged();
        }

       /* public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (ExpenseType == ExpenseType.Income && Amount < 0)
            {
                errors.Add(new ValidationResult("Income cant be lesser than zero", new[] { nameof(Amount) }));
            }
            else if (ExpenseType == ExpenseType.Expense && Amount > 0)
            {
                errors.Add(new ValidationResult("Income cant be greater than zero", new[] { nameof(Amount) }));
            }

            return errors;
        }*/
      
    }
}
