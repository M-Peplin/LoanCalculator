using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;

namespace LoanCalcAPI.Services.Interfaces
{
    public interface ILoanService
    {
        /// <summary>
        /// Gets full list of loan types from database
        /// </summary>
        public Task<OperationSuccessDTO<List<Loan>>> GetLoansAsync();
        /// <summary>
        /// Updating database loan interest based on provided Id
        /// </summary>
        public Task<OperationResultDTO> UpdateLoanInterestByIdAsync(int idLoan, double interest);
        
    }
}
