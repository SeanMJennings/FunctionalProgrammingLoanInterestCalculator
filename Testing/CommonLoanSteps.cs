using System.Globalization;
using Domain.Entities;
using FluentAssertions;
using FunctionalProgrammingKit;
using NodaTime;
using Utilities;

namespace Testing;

public static class CommonLoanSteps
{
    public const int loan_id = 1;
    public const string US_code = "US";
    public const string US_iso_currency_symbol = "USD";
    public const string start_date_value = "2022-01-01";
    public const string end_date_value = "2022-12-31";
    public const decimal amount_value = 1000m;
    public const decimal base_interest_rate_value = 5m;
    public const decimal margin_interest_rate_value = 2.5m;
    
    public static object a_start_date()
    {
        var builder = new LoanBuilder();
        return builder.WithStartDate(LocalDate.FromDateTime(DateTime.Parse(start_date_value)));
    }

    public static object an_end_date(object builder)
    {
        ((LoanBuilder)builder).WithEndDate(LocalDate.FromDateTime(DateTime.Parse(end_date_value)));
        return ((LoanBuilder)builder).WithAccrualDate(Date.Now.Day);
    }

    public static object an_amount(object builder)
    {
        return ((LoanBuilder)builder).WithAmount(amount_value);
    }

    public static object a_currency(object builder)
    {
        return ((LoanBuilder)builder).WithCurrency(US_code);
    }

    public static object a_base_interest_rate(object builder)
    {
        return ((LoanBuilder)builder).WithBaseInterestRate(base_interest_rate_value);
    }

    public static object a_margin_interest_rate(object builder)
    {
        return ((LoanBuilder)builder).WithMarginInterestRate(margin_interest_rate_value);
    }
    
    public static object the_loan_is_created(object loan)
    {
        var _loan = ((ReturnWrapper<Loan>)loan).Value;
        _loan.Should().NotBeNull();
        _loan.Id.Should().Be(loan_id);
        _loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(start_date_value);
        _loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(end_date_value);
        ((decimal)_loan.Amount).Should().Be(amount_value);
        _loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        _loan.BaseInterestRate.Should().Be(base_interest_rate_value);
        _loan.MarginInterestRate.Should().Be(margin_interest_rate_value);
        return loan;
    }

    public static void is_formatted_correctly(object loan)
    {
        var _loan = ((ReturnWrapper<Loan>)loan).Value;
         var expected_formatting = $"""
             Loan ID: {_loan.Id}
             Period: {_loan.StartDate:yyyy-MM-dd} to {_loan.EndDate:yyyy-MM-dd}
             Value: {_loan.Amount} {_loan.Currency}
             Base interest rate: {_loan.BaseInterestRate}
             Margin interest rate: {_loan.MarginInterestRate}
             Accrual Date: Day {_loan.AccrualDate} of month
             Daily interest without margin: {Math.Round(amount_value * base_interest_rate_value / 100 / 365, 2)} {_loan.Currency}
             Daily interest: {Math.Round(amount_value * (base_interest_rate_value + margin_interest_rate_value) / 100 / 365, 2)} {_loan.Currency}
             Days elapsed: {Period.Between(Date.ParseDate(start_date_value), Date.Now, PeriodUnits.Days).Days}
             Total interest: {Math.Round(amount_value * (base_interest_rate_value + margin_interest_rate_value) / 100 / 365 * Period.Between(Date.ParseDate(start_date_value), Date.ParseDate(end_date_value), PeriodUnits.Days).Days,2)} {_loan.Currency}
             """;
         _loan.ToString().Should().Be(expected_formatting);
    }
}