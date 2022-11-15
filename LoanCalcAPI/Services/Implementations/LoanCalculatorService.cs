using LoanCalcAPI.Data;
using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;
using LoanCalcAPI.Services.Interfaces;
using LoanCalcAPI.Helpers;
using LoanCalcAPI.Controllers;

namespace LoanCalcAPI.Services.Implementations
{
    public class LoanCalculatorService : ILoanCalculatorService
    {
        private readonly LoanContext _loanContext;
        private readonly ILogger<LoanCalculatorService> _logger;
        public LoanCalculatorService(LoanContext loanContext, ILogger<LoanCalculatorService> logger)
        {
            this._loanContext = loanContext;
            this._logger = logger;
        }

        public async Task<OperationResultDTO> CalculateLoanCostAsync(int idLoan, double loanAmount, double numberOfYears)
        {
            _logger.LogInformation("Executing method CalculateLoanCostAsync");

            var loanParams = await _loanContext.Loan.FindAsync(idLoan);
            if (loanParams == null)
            {
                return new OperationErrorDTO { Code = 404, Message = $"Loan with id {idLoan} doesn't exist" };
            }

            LoanBase loan = LoanTypes.GetLoanType((LoanTypes.LoanType)idLoan);

            var paymentAmount = loan.CalculateLoanCost(loanParams, loanAmount, numberOfYears);

            if(paymentAmount > 0)
            {
                return new OperationSuccessDTO<ResultCostDTO>
                {
                    Message = "Success",
                    Result = new ResultCostDTO { Cost = paymentAmount }
                };
            }
            return new OperationErrorDTO
            {
                Message = "Wrong parameters provided"                
            };
        }


    }
}
