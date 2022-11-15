using LoanCalcAPI.DTOs;
using LoanCalcAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalcAPI.Tests
{
    public class TestHelper
    {
        public Random rand = new Random();

        public OperationSuccessDTO<List<Loan>> CreateRandomLoanListDTO()
        {
            return new()
            {
                Message = "Success",
                Result = new List<Loan>()
                {
                    new Loan() { IdLoan = rand.Next(1000), Interest = rand.Next(100), LoanName = CreateRandomString(rand.Next(20)) }                    
                }
            };
        }

        public List<Loan> CreateRandomLoanList()
        {
            return new List<Loan>()
            {
                new Loan() { IdLoan = rand.Next(1000), Interest = rand.Next(100), LoanName = CreateRandomString(rand.Next(20)) },
                new Loan() { IdLoan = rand.Next(1000), Interest = rand.Next(100), LoanName = CreateRandomString(rand.Next(20)) }
            };
        }

        public  Loan CreateRandomLoan()
        {
            return new()
            {
                IdLoan = rand.Next(1000),
                Interest = rand.Next(100),
                LoanName = CreateRandomString(rand.Next(20)),
            };
        }

        public OperationSuccessDTO<Loan> CreateRandomLoanDTO()
        {
            return new()
            {
                Message = "Success",
                Result = CreateRandomLoan()
            };

        }

        public OperationSuccessDTO<ResultCostDTO> CreateRandomCostDTO()
        {
            return new()
            {
                Message = "Success",
                Result = new ResultCostDTO()
                {
                    Cost = rand.Next(100000)
                }
            };
        }

        public OperationErrorDTO CreateOperationErrorDTO()
        {
            return new OperationErrorDTO
            {
                Code = 404,
                Message = "Error"
            };
        }
        public string CreateRandomString(int size)
        {
            var builder = new StringBuilder(size);


            char offset = 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)rand.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();
        }
    }
}
