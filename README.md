# Functional Programming - Loan Interest Calculator
Re-implement the Loan Interest Calculator for banking customers using functional programming, domain-driven design, and test-driven development.
## Guidelines
- Produce a small console application.
- Use an in-memory store.
- Simple interest formula: https://www.investopedia.com/terms/s/simple_interest.asp
## User Journey
1. A user can provide input parameters to calculate a loan.
2. The system should generate an output containing the loan calculation results.
3. A user can access historic calculations that they can update with new input parameters.
## The input for a loan should contain:
1. Start Date (date)
2. End Date (date)
3. Loan Amount (amount field)
4. Loan Currency (currency)
5. Base Interest Rate (percentage)
6. Margin (percentage), where Total Interest Rate = Base Interest Rate + Margin
## The output data structure should include the daily accrued interest for each day between the start and end date of the loan:
1. Daily Interest Amount without margin
2. Daily Interest Amount Accrued
3. Accrual Date
4. Number of Days elapsed since the Start Date of the loan
5. Total Interest - calculated over the given period

## Approach
- Functional Programming information - https://www.bastienfoucher.net/series/functional-ddd-csharp
- Functional programming approach inspired by https://github.com/BastienFoucher/Grenat.Functional.DDD
