using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorExpenseTracker.Model.Validations
{
    public class ExpenseTransactionDateValidator : ValidationAttribute
    {
        public int DaysInTheFuture { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime transactionDate;
            if (DateTime.TryParse(value.ToString(), out transactionDate))
            {
                if (transactionDate == DateTime.MinValue)
                    return new ValidationResult("Date shouldnt be empty", new[] {validationContext.MemberName});
                if (transactionDate > DateTime.Now.AddDays(DaysInTheFuture))
                    return new ValidationResult($"Date can be greater than today plus {DaysInTheFuture}",
                        new[] {validationContext.MemberName});

                return null;
            }

            return new ValidationResult("Invalid Date", new[] {validationContext.MemberName});
        }
    }
}