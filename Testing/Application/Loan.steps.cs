using System.Globalization;
using Application;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using Persistence;
using Utilities;

namespace Testing.Application;

[TestFixture]
public static partial class LoanSpecsShould
{
    private static void after_each()
    {
        Repository._loans.Clear();
    }
    
    private static CreateLoanDto a_valid_loan_dto()
    {
        return new CreateLoanDto(Date.ParseDate(start_date_value), Date.ParseDate(end_date_value), amount_value, US_code, base_interest_rate_value, margin_interest_rate_value);
    }    
    
    private static CreateLoanDto an_invalid_loan_dto()
    {
        return new CreateLoanDto(Date.ParseDate(end_date_value), Date.ParseDate(start_date_value), amount_value, US_code, base_interest_rate_value, margin_interest_rate_value);
    }    
    
    private static CreateLoanDto another_valid_loan_dto()
    {
        return new CreateLoanDto(Date.ParseDate(new_start_date_value), Date.ParseDate(new_end_date_value), new_amount_value, GB_code, new_base_interest_rate_value, new_margin_interest_rate_value);
    }
    
    private static object creating_a_loan(object loanDto)
    {
        return LoanFunctions.CreateLoan((CreateLoanDto)loanDto, Repository.NextId, Repository.SaveLoan);
    }    
    
    private static object updating_a_loan()
    {
        var loanDto = new UpdateLoanDto(loan_id, Date.ParseDate(new_start_date_value), Date.ParseDate(new_end_date_value), new_amount_value, GB_code, new_base_interest_rate_value, new_margin_interest_rate_value);
        return LoanFunctions.UpdateLoan(loanDto, Repository.UpdateLoan);
    }    
    
    private static object listing_loans()
    {
        return LoanFunctions.ListLoans(Repository.GetLoans);
    }     
    
    private static object a_loan()
    {
        var loanDto = a_valid_loan_dto();
        return creating_a_loan(loanDto);
    }
    
    private static object another_loan()
    {
        var loanDto = another_valid_loan_dto();
        return creating_another_loan(loanDto);
    }
    
    private static ResponseDto<Loan> creating_another_loan(CreateLoanDto loanDto)
    {
        return LoanFunctions.CreateLoan(loanDto, Repository.NextId, Repository.SaveLoan);
    }
    
    private static string WhenValidating(this object previousResult, Func<object, object> func)
    {
        return previousResult.WhenValidatingTheResponseDto<Loan>(func);
    }
    
    private static void the_created_loan_is_listed()
    {
        the_loan_is_valid(LoanFunctions.ListLoans(Repository.GetLoans).First().Data);
        after_each();
    }    
    
    private static void the_updated_loan_is_listed(object loanDto)
    {
        the_loan_is_updated(LoanFunctions.ListLoans(Repository.GetLoans).First().Data);
        after_each();
    }    
    
    private static void the_listed_loans_are_correct(object loans)
    {
        var _loans = (IEnumerable<ResponseDto<Loan>>)loans;
        _loans.Count().Should().Be(2);
        the_loan_is_valid(_loans.First().Data);
        another_loan_is_valid(_loans.Last().Data);
        after_each();
    }
    
    private static void the_loan_is_updated(Loan loan)
    {
        loan.Should().NotBeNull();
        loan.Id.Should().Be(loan_id);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(new_start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(new_end_date_value);
        ((decimal)loan.Amount).Should().Be(new_amount_value);
        loan.Currency.ToString().Should().Be(GB_iso_currency_symbol);
        loan.BaseInterestRate.Should().Be(new_base_interest_rate_value);
        loan.MarginInterestRate.Should().Be(new_margin_interest_rate_value);
    }
}