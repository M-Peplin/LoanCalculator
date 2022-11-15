using FluentAssertions;
using LoanCalcAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalcAPI.Tests.Models
{
    public class HousingLoanTests
    {
        HousingLoan housingLoan = new HousingLoan();
        TestHelper testHelper = new TestHelper();
        Random rand = new Random();


        [Theory]
        [InlineData(100000, 30, 449.04)]
        [InlineData(245000, 25, 1226.53)]
        [InlineData(65000, 5, 1182.46)]
        //Expected value based on totalmoney.pl loan calculator results for equal parameters
        public void CalculateLoanCost_WithCorrectArguments_ShouldCalculateProperValue(double loanAmount, int numberOfYears, double expected)
        {
            //Arrange
            Loan loan = new Loan() { IdLoan = 2, Interest = 3.5, LoanName = testHelper.CreateRandomString(rand.Next(10)) };

            //Act
            double actual = housingLoan.CalculateLoanCost(loan, loanAmount, numberOfYears);
            actual = Math.Round(actual, 2);
            //Assert
            Assert.Equal(expected, System.Math.Round(actual, 2));
        }

        [Theory]
        [InlineData(-100000, 30)]
        [InlineData(245000, -25)]
        [InlineData(-65000, -5)]
        public void CalculateLoanCost_WithNegativeNumberArgument_ShouldReturnZero(double loanAmount, int numberOfYears)
        {

            //Arrange
            Loan loan = new Loan() { IdLoan = 2, Interest = 3.5, LoanName = testHelper.CreateRandomString(rand.Next(10)) };
            

            //Act
            double actual = housingLoan.CalculateLoanCost(loan, loanAmount, numberOfYears);
            
            //Assert
            Assert.Equal(0, actual);
        }

        [Theory]
        [InlineData(0, 30)]
        [InlineData(245000, 0)]        
        public void CalculateLoanCost_WithZeroArgument_ShouldReturnZero(double loanAmount, int numberOfYears)
        {

            //Arrange
            Loan loan = new Loan() { IdLoan = 2, Interest = 3.5, LoanName = testHelper.CreateRandomString(rand.Next(10)) };


            //Act
            double actual = housingLoan.CalculateLoanCost(loan, loanAmount, numberOfYears);

            //Assert
            Assert.Equal(0, actual);
        }

        [Theory]
        [InlineData(double.MaxValue, int.MaxValue)]        
        public void CalculateLoanCost_ProvidingMaximumValues_ShouldReturnGreaterThanZeroValue(double loanAmount, int numberOfYears)
        {

            //Arrange
            Loan loan = new Loan() { IdLoan = 2, Interest = 3.5, LoanName = testHelper.CreateRandomString(rand.Next(10)) };


            //Act
            double actual = housingLoan.CalculateLoanCost(loan, loanAmount, numberOfYears);

            //Assert
            actual.Should().BeGreaterThan(0);
        }
    }
}
