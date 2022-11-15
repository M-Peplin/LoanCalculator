using LoanCalcAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace LoanCalcAPI.Models
{
    /// <summary>
    /// Base class of loan, that particular loan types should derive from
    /// </summary>
    public abstract class LoanBase
    {
        /// <summary>
        /// Method for calculation of monthly payback plan. Loan types may have different calculation methods.
        /// </summary>
        public abstract double CalculateLoanCost(Loan loan, double loanAmount, double numberOfYears);
    }
}
