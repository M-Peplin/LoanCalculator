using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;
using LoanCalcAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoanCalcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {   
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService _loanService;
        private readonly ILoanCalculatorService _loanCalculatorService;

        public LoanController(ILogger<LoanController> logger, ILoanService loanService, ILoanCalculatorService loanCalculatorService)
        {
            this._logger = logger;
            this._loanService = loanService; 
            this._loanCalculatorService = loanCalculatorService;    
        }

        [HttpGet]
        [Route("GetLoans")]
        public async Task<ActionResult<OperationSuccessDTO<List<Loan>>>> GetLoansAsync()
        {
            _logger.LogInformation("Executing controller method GetLoansAsync");

            var loans = await  _loanService.GetLoansAsync();

            return (loans.Result.Any() ? Ok(loans) : NotFound(loans.Message));
        }

        [HttpGet]
        [Route("CalculateLoanCost/{idLoan}/{years}/{amount}")]

        public async Task<ActionResult<ResultCostDTO>> CalculateLoanCostAsync(int idLoan, int years, double amount)
        {
            _logger.LogInformation($"Calculating loan cost for loan {idLoan} with params: years = {years}, amount = {amount}");

            var response = await _loanCalculatorService.CalculateLoanCostAsync(idLoan, amount, years);
            
            return (response.Message.Equals("Success")? Ok(response) : NotFound(response.Message));
        }

        [HttpPut]
        [Route("UpdateLoanInterestById")]
        public async Task<ActionResult<Loan>> UpdateLoanInterestAsync(Loan loan)
        {
            _logger.LogInformation($"Updating loan {loan.IdLoan}");
            var response = await _loanService.UpdateLoanInterestByIdAsync(loan.IdLoan, loan.Interest);

            return (response.Message.Equals("Success") ? Ok(response) : NotFound(response.Message));
        }
    }
}