using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;

namespace LoanCalcAPI.Services.Interfaces
{
    public interface ILoanCalculatorService
    {
        /// <summary>
        /// Method for calculation of monthly payback plan. Loan types may have different calculation methods.
        /// </summary>
        public Task <OperationResultDTO> CalculateLoanCostAsync(int idLoan, double loanAmount, double numberOfYears);        
    }
}
