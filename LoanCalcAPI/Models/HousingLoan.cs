using LoanCalcAPI.Data;
using LoanCalcAPI.DTOs;
using LoanCalcAPI.Services.Interfaces;

namespace LoanCalcAPI.Models
{
    public class HousingLoan : LoanBase
    {
        public override double CalculateLoanCost(Loan loan, double loanAmount, double numberOfYears)
        {            
            if(loanAmount <= 0 || numberOfYears <= 0) return 0;

            var interest = loan.Interest;

            // rate of interest and number of payments for monthly payments
            var rateOfInterest = loan.Interest / 1200;
            var numberOfPayments = numberOfYears * 12;
          
            double paymentAmount = (rateOfInterest * loanAmount) / (1 - Math.Pow(1 + rateOfInterest, numberOfPayments * -1));

            return (paymentAmount > 0 ? paymentAmount : 0);
        }
    }
}
