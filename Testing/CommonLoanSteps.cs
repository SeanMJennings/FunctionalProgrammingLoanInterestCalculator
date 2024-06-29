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
    public const int another_loan_id = 1;
    public const string US_code = "US";
    public const string GB_code = "GB";
    public const string US_iso_currency_symbol = "USD";
    public const string GB_iso_currency_symbol = "GBP";
    public const string start_date_value = "2022-01-01";
    public const string new_start_date_value = "2023-01-01";
    public const string end_date_value = "2022-12-31";
    public const string new_end_date_value = "2024-12-31";
    public const decimal amount_value = 1000m;
    public const decimal new_amount_value = 2000m;
    public const decimal base_interest_rate_value = 5m;
    public const decimal new_base_interest_rate_value = 7m;
    public const decimal margin_interest_rate_value = 2.5m;
    public const decimal new_margin_interest_rate_value = 2.9m;
    
    public static object a_start_date()
    {
        var builder = new LoanBuilder();
        return builder.WithStartDate(Date.ParseDate(start_date_value));
    }

    public static object an_end_date(object builder)
    {
        ((LoanBuilder)builder).WithEndDate(Date.ParseDate(end_date_value));
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
        return the_loan_is_valid(_loan);
    }    
    
    public static object the_loan_is_valid(Loan loan)
    {
        loan.Should().NotBeNull();
        loan.Id.Should().Be(loan_id);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(end_date_value);
        ((decimal)loan.Amount).Should().Be(amount_value);
        loan.Currency.ToString().Should().Be(US_iso_currency_symbol);
        loan.BaseInterestRate.Should().Be(base_interest_rate_value);
        loan.MarginInterestRate.Should().Be(margin_interest_rate_value);
        return loan;
    }    
    
    public static void another_loan_is_valid(Loan loan)
    {
        loan.Should().NotBeNull();
        loan.Id.Should().Be(another_loan_id);
        loan.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(new_start_date_value);
        loan.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture).Should().Be(new_end_date_value);
        ((decimal)loan.Amount).Should().Be(new_amount_value);
        loan.Currency.ToString().Should().Be(GB_iso_currency_symbol);
        loan.BaseInterestRate.Should().Be(new_base_interest_rate_value);
        loan.MarginInterestRate.Should().Be(new_margin_interest_rate_value);
    }

    public static void is_formatted_correctly(object loan)
    {
        var _loan = (Loan)loan;
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