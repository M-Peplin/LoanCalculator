# LoanCalculator
## This API was designed to calculate monthly loan payback plan.

Testing solution:

1. Run T-SQL script (..\LoanCalcAPI\Scripts\InitialScript) to provide a schema and initial DB record.
2. Set connection string "DefaultConnection" in appSettings.json to match your DB configuration.

API provides three endpoints:
1) Get /Loan/GetLoans - returns full list of loan types from database.
2) Get /Loan/CalculateLoanCost/{idLoan}/{years}/{amount} - Calculate monthly loan installment based on loan type. User needs to specify payback time in years and desired amount of money.
3) Put /Loan/UpdateLoanInterestById - Assuming that interest is not a constant value, this endpoint provides the ability to change interest percentages in DB.

Screenshots:

![calculate](https://user-images.githubusercontent.com/79601132/201850028-fa5707cb-435a-4e75-b70a-050e10b2729b.png)

Results compared to other calculator provided by site totalmoney.pl
![comparsion](https://user-images.githubusercontent.com/79601132/201850055-3f3abd26-1ada-49e9-b508-24fbcd69ab04.png)

![Get](https://user-images.githubusercontent.com/79601132/201850064-101c6ae5-4c16-47d4-a883-c6148beeb349.png)

![put](https://user-images.githubusercontent.com/79601132/201850089-ae99c7c7-a21a-4779-9e1a-28f47fa1d8f2.png)

![Tests](https://user-images.githubusercontent.com/79601132/201850101-09347886-542c-4052-8e6e-f64e742303d8.png)
