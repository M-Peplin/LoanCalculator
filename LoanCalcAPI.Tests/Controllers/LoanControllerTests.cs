using Castle.Core.Logging;
using LoanCalcAPI.Controllers;
using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;
using LoanCalcAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace LoanCalcAPI.Tests.Controllers
{
    public class LoanControllerTests
    {
        private readonly Mock<ILoanService> loanServiceStub = new Mock<ILoanService>();

        private readonly Mock<ILoanCalculatorService> calculatorServiceStub = new();

        private readonly Mock<ILogger<LoanController>> loggerStub = new();

        private readonly TestHelper helper = new TestHelper();

        private readonly Random rand = new Random();


        [Fact]
        public async Task GetLoansAsync_WithUnexistingItem_ReturnsNotFound()
        {
            //Arrange           
                              
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(new OperationSuccessDTO<List<Loan>> { Message = "Msg", Result = new List<Loan> { } });
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.GetLoansAsync();
            var notFoundObj = result.Result as NotFoundObjectResult;

            //Assert
            notFoundObj.Should().BeOfType<NotFoundObjectResult>();

                       
        }

        [Fact]

        public async Task GetLoansAsync_WithExistingItems_ReturnsAllItems()
        {
            //Arrange
            var expectedItems = helper.CreateRandomLoanListDTO();
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(expectedItems);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);


            //Act
            var result = await controllerSUT.GetLoansAsync();
            var okObj = result.Result as OkObjectResult;

            //Assert          
            okObj.Value.Should().BeEquivalentTo(expectedItems, options => options.ComparingByMembers<OperationSuccessDTO<List<Loan>>>());

        }

        [Fact]

        public async Task GetLoansAsync_OnSuccess_ShouldBeOfExpectedType()
        {
            //Arrange
            var expectedItems = helper.CreateRandomLoanListDTO();
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(expectedItems);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.GetLoansAsync();


            //Assert
            result.Should().BeOfType<ActionResult<OperationSuccessDTO<List<Loan>>>>();
        }

        [Fact]
        public async Task GetLoansAsync_OnSuccess_InvokesLoanServiceOnce()
        {
            //Arrange
            var expectedItems = helper.CreateRandomLoanListDTO();
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(expectedItems);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.GetLoansAsync();

            //Assert
            loanServiceStub.Verify(service => service.GetLoansAsync(), Times.Once());
        }

        [Fact]
        public async Task GetLoansAsync_OnSuccess_ShouldReturnCode200()
        {
            //Arrange            
            var expectedItems = helper.CreateRandomLoanListDTO();
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(expectedItems);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.GetLoansAsync();
            var okObj = result.Result as OkObjectResult;

            //Assert
            okObj.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetLoansAsync_WithUnxistingItems_ShouldReturnCode404()
        {
            //Arrange
            var emptyLoanListObject = new OperationSuccessDTO<List<Loan>> { Message = "There is no loans in database", Result = new List<Loan>() };            
            loanServiceStub.Setup(x => x.GetLoansAsync()).ReturnsAsync(emptyLoanListObject);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.GetLoansAsync();
            var notFoundObj = result.Result as NotFoundObjectResult;

            //Assert
            notFoundObj.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateLoanInterestAsync_OnSuccess_InvokesLoanServiceOnce()
        {
            //Arrange
            var randomLoanDTO = helper.CreateRandomLoanDTO();
            var randomLoan = helper.CreateRandomLoan();
            loanServiceStub.Setup(x => x.UpdateLoanInterestByIdAsync(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(randomLoanDTO);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.UpdateLoanInterestAsync(randomLoan);

            //Assert
            loanServiceStub.Verify(service => service.UpdateLoanInterestByIdAsync(randomLoan.IdLoan, randomLoan.Interest), Times.Once());
        }

        [Fact]
        public async Task UpdateLoanInterestAsync_OnSuccess_ShouldReturnCode200()
        {
            //Arrange
            var randomLoanDTO = helper.CreateRandomLoanDTO();
            var randomLoan = helper.CreateRandomLoan();
            loanServiceStub.Setup(x => x.UpdateLoanInterestByIdAsync(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(randomLoanDTO);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.UpdateLoanInterestAsync(randomLoan);
            var okObj = result.Result as OkObjectResult;

            //Assert
            okObj.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task UpdateLoanInterestAsync_WithNonExistingLoan_ShouldReturnCode404()
        {
            //Arrange
            var error = helper.CreateOperationErrorDTO();
            var randomLoanDTO = helper.CreateRandomLoanDTO();
            var randomLoan = helper.CreateRandomLoan();
            loanServiceStub.Setup(x => x.UpdateLoanInterestByIdAsync(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(error);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.UpdateLoanInterestAsync(randomLoan);
            var notFoundObj = result.Result as NotFoundObjectResult;

            //Assert
            notFoundObj.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateLoanInterestAsync_OnSuccess_ShouldBeOfTypeActionResultLoan()
        {
            //Arrange
            var randomLoanDTO = helper.CreateRandomLoanDTO();
            var randomLoan = helper.CreateRandomLoan();
            loanServiceStub.Setup(x => x.UpdateLoanInterestByIdAsync(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(randomLoanDTO);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);


            //Act
            var result = await controllerSUT.UpdateLoanInterestAsync(randomLoan);

            //Assert
            result.Should().BeOfType<ActionResult<Loan>>();
        }

        [Fact]
        public async Task UpdateLoanInterestAsync_WithUnexistingLoan_ShouldReturnNotFound()
        {
            //Arrange
            var error = helper.CreateOperationErrorDTO();
            var randomLoan = helper.CreateRandomLoan();
            loanServiceStub.Setup(x => x.UpdateLoanInterestByIdAsync(It.IsAny<int>(), It.IsAny<double>())).ReturnsAsync(error);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);


            //Act
            var result = await controllerSUT.UpdateLoanInterestAsync(randomLoan);
            var notFoundObj = result.Result as NotFoundObjectResult;

            //Assert
            notFoundObj.Should().BeOfType<NotFoundObjectResult>();

        }

        [Fact]
        public async Task CalculateLoanCostAsync_OnSuccess_InvokesCalculatorServiceOnce()
        {
            //Arrange
            var randomLoan = helper.CreateRandomLoan();
            var randomCost = helper.CreateRandomCostDTO();
            int years = rand.Next(50);
            double amount = rand.Next(10000000);
            calculatorServiceStub.Setup(x => x.CalculateLoanCostAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(randomCost);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act

            var result = await controllerSUT.CalculateLoanCostAsync(randomLoan.IdLoan, years, amount);
            //Assert

            calculatorServiceStub.Verify(service => service.CalculateLoanCostAsync(randomLoan.IdLoan, amount, years), Times.Once());
        }

        [Fact]
        public async Task CalculateLoanCostAsync_OnSuccess_ShouldBeOfTypeActionResultResultCostDTO()
        {
            //Arrange
            var randomLoan = helper.CreateRandomLoan();
            var randomCost = helper.CreateRandomCostDTO();
            int years = rand.Next(50);
            double amount = rand.Next(10000000);
            calculatorServiceStub.Setup(x => x.CalculateLoanCostAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(randomCost);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.CalculateLoanCostAsync(randomLoan.IdLoan, years, amount);
            //Assert

            result.Should().BeOfType<ActionResult<ResultCostDTO>>();
        }

        [Fact]
        public async Task CalculateLoanCostAsync_OnSuccess_ShouldReturnCode200()
        {
            //Arrange
            var randomLoan = helper.CreateRandomLoan();
            var randomCost = helper.CreateRandomCostDTO();
            int years = rand.Next(50);
            double amount = rand.Next(10000000);
            calculatorServiceStub.Setup(x => x.CalculateLoanCostAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(randomCost);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.CalculateLoanCostAsync(randomLoan.IdLoan, years, amount);
            var okObj = result.Result as OkObjectResult;
            //Assert

            okObj.StatusCode.Should().Be(200);
        }


        [Fact]
        public async Task CalculateLoanCostAsync_WithUnxistingLoan_ShouldReturnCode404()
        {
            //Arrange
            var randomLoan = helper.CreateRandomLoan();
            int years = rand.Next(50);
            double amount = rand.Next(10000000);
            var operationError = helper.CreateOperationErrorDTO;
            calculatorServiceStub.Setup(x => x.CalculateLoanCostAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(operationError);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.CalculateLoanCostAsync(randomLoan.IdLoan, years, amount);
            var notFoundObj = result.Result as NotFoundObjectResult;
            //Assert

            notFoundObj.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CalculateLoanCostAsync_WithUnxistingLoan_ShouldBeOfTypeNotFound()
        {
            //Arrange
            var randomLoan = helper.CreateRandomLoan();
            int years = rand.Next(50);
            double amount = rand.Next(10000000);
            var operationError = helper.CreateOperationErrorDTO;
            calculatorServiceStub.Setup(x => x.CalculateLoanCostAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(operationError);
            var controllerSUT = new LoanController(loggerStub.Object, loanServiceStub.Object, calculatorServiceStub.Object);

            //Act
            var result = await controllerSUT.CalculateLoanCostAsync(randomLoan.IdLoan, years, amount);
            var notFoundObj = result.Result as NotFoundObjectResult;

            //Assert
            notFoundObj.Should().BeOfType<NotFoundObjectResult>();
        }

    }
}