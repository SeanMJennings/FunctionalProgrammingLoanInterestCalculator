using System.Collections.Immutable;
using System.Globalization;
using Application;
using Application.RequestDtos;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using Utilities;

namespace Testing.Application;

[TestFixture]
public static partial class LoanSpecsShould
{
    private static readonly List<Loan> loan_repository = [];
    private static void after_each()
    {
        loan_repository.Clear();
    }
    
    private static CreateLoanDto a_valid_loan_dto()
    {
        return new CreateLoanDto(Date.ParseDate(start_date_value), Date.ParseDate(end_date_value), amount_value, US_code, base_interest_rate_value, margin_interest_rate_value);
    }    
    
    private static CreateLoanDto another_valid_loan_dto()
    {
        return new CreateLoanDto(Date.ParseDate(new_start_date_value), Date.ParseDate(new_end_date_value), new_amount_value, GB_code, new_base_interest_rate_value, new_margin_interest_rate_value);
    }
    
    private static ResponseDto<Loan> creating_a_loan(object loanDto)
    {
        var saveLoan = new Func<Loan, Loan>(loan =>
        {
            loan_repository.Add(loan);
            return loan;
        });
        return LoanFunctions.CreateLoan((CreateLoanDto)loanDto, () => loan_id, saveLoan);
    }    
    
    private static object updating_a_loan()
    {
        var loanDto = new UpdateLoanDto(loan_id, Date.ParseDate(new_start_date_value), Date.ParseDate(new_end_date_value), new_amount_value, GB_code, new_base_interest_rate_value, new_margin_interest_rate_value);
        var updateLoan = new Func<Loan, Loan>(loan =>
        {
            var existing_loan = loan_repository[0];
            loan_repository[0] = Loan.Create(existing_loan.Id, loan.StartDate, loan.EndDate, existing_loan.AccrualDate,
                loan.Amount, loan.Currency.Code, loan.BaseInterestRate, loan.MarginInterestRate).Match().Value;
            return loan_repository[0];
        });
        return LoanFunctions.UpdateLoan(loanDto, updateLoan);
    }    
    
    private static object listing_loans()
    {
        var the_loans = new Func<IEnumerable<Loan>>(() => loan_repository.ToImmutableArray());
        return LoanFunctions.ListLoans(the_loans);
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
        var saveLoan = new Func<Loan, Loan>(loan =>
        {
            loan_repository.Add(loan);
            return loan;
        });
        return LoanFunctions.CreateLoan(loanDto, () => another_loan_id, saveLoan);
    }
    
    private static void the_created_loan_is_listed()
    {
        var getLoans = new Func<IEnumerable<Loan>>(() => loan_repository);
        the_loan_is_valid(LoanFunctions.ListLoans(getLoans).First().Data);
        after_each();
    }    
    
    private static void the_updated_loan_is_listed(object loanDto)
    {
        var getLoans = new Func<IEnumerable<Loan>>(() => loan_repository);
        the_loan_is_updated(LoanFunctions.ListLoans(getLoans).First().Data);
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