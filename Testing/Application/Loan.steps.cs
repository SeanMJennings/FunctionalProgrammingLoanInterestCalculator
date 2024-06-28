using Application;
using Application.RequestDtos;
using Domain.Entities;
using NUnit.Framework;
using Utilities;

namespace Testing.Application;

[TestFixture]
public static partial class LoanSpecsShould
{
    private static readonly List<Loan> loan_repository = [];
    private static object a_valid_loan_dto()
    {
        return new LoanDto(Date.ParseDate(start_date_value), Date.ParseDate(end_date_value), amount_value, US_code, base_interest_rate_value, margin_interest_rate_value);
    }
    
    private static object creating_a_loan(object loanDto)
    {
        var saveLoan = new Func<Loan, Loan>(loan =>
        {
            loan_repository.Add(loan);
            return loan;
        });
        return LoanFunctions.CreateLoan((LoanDto)loanDto, () => loan_id, saveLoan);
    }    
    
    private static void the_loan_is_listed(object loanDto)
    {
        var getLoans = new Func<IEnumerable<Loan>>(() => loan_repository);
        the_loan_is_valid(LoanFunctions.ListLoans(getLoans).First().Data);
        loan_repository.Clear();
    }
}