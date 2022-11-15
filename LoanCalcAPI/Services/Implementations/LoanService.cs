using LoanCalcAPI.Data;
using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;
using LoanCalcAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoanCalcAPI.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly LoanContext _loanContext;
        private readonly ILogger<LoanService> _logger; 

        public LoanService(LoanContext loanContext, ILogger<LoanService> logger)
        {
            this._loanContext = loanContext;
            _logger = logger;   
        }

        public async Task<OperationSuccessDTO<List<Loan>>> GetLoansAsync()
        {
            _logger.LogInformation("Executing method GetLoansAsync");

            var loans = await _loanContext.Loan.ToListAsync();
            if(!loans.Any())
            {
                return new OperationSuccessDTO<List<Loan>> { Message = "There is no loans in database", Result = new List<Loan>()};
            }
            
            return new OperationSuccessDTO<List<Loan>> { Message = "Success", Result = loans };            
        }

        public async Task<OperationResultDTO> UpdateLoanInterestByIdAsync(int idLoan, double interest)
        {
            _logger.LogInformation("Executing method UpdateLoanInterestByIdAsync");

            var updateLoan = await _loanContext.Loan.FindAsync(idLoan);
            if(updateLoan == null)
            {
                return new OperationErrorDTO { Code = 404, Message = $"Loan with ID {idLoan} doesn't exist" };
            }
            updateLoan.Interest = interest;

            await _loanContext.SaveChangesAsync();

            return new OperationSuccessDTO<Loan> { Message = "Success", Result = updateLoan };
        }
    }
}
