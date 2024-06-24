using NUnit.Framework;

namespace Testing.Entities;

public static partial class LoanSpecsShould
{
    [Test]
    public static void allow_creation_of_valid_loan()
    {
        Given(a_start_date)
            .And(an_end_date)
            .And(an_amount)
            .And(a_currency)
            .And(a_base_interest_rate)
            .And(a_margin_interest_rate)
            .When(creating_a_loan)
            .Then(the_loan_is_created)
            .And(is_formatted_correctly);
    }
    
    [Test]
    public static void ensure_end_date_must_be_greater_than_start_date()
    {
        Given(a_start_date)
            .And(an_end_date_equal_to_start_date)
            .And(an_amount)
            .And(a_currency)
            .And(a_base_interest_rate)
            .And(a_margin_interest_rate)
            .WhenValidating(creating_a_loan)
            .ThenInforms("End date must be after start date.");
    }
    
    [Test]
    public static void ensure_loan_value_must_be_greater_than_zero()
    {
        Given(a_start_date)
            .And(an_end_date)
            .And(an_amount_equal_to_zero)
            .And(a_currency)
            .And(a_base_interest_rate)
            .And(a_margin_interest_rate)
            .WhenValidating(creating_a_loan)
            .ThenInforms("Amount must be greater than zero.");
    }
    
    [Test]
    public static void ensure_total_interest_rate_must_be_greater_than_zero()
    {
        Given(a_start_date)
            .And(an_end_date)
            .And(an_amount)
            .And(a_currency)
            .And(a_total_interest_rate_equal_to_zero)
            .WhenValidating(creating_a_loan)
            .ThenInforms("Total interest rate must be greater than zero.");
    }
}