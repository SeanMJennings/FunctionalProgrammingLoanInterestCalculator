using Domain.Entities;
using NodaTime;
using NUnit.Framework;

namespace Testing.Domain.Entities;

[TestFixture]
public static partial class LoanSpecsShould
{
    private static object an_end_date_equal_to_start_date(object builder)
    {
        return ((LoanBuilder)builder).WithStartDate(LocalDate.FromDateTime(DateTime.Parse(start_date_value)))
            .WithEndDate(LocalDate.FromDateTime(DateTime.Parse(start_date_value)));
    }

    private static object an_amount_equal_to_zero(object builder)
    {
        return ((LoanBuilder)builder).WithAmount(0);
    }

    private static object a_total_interest_rate_equal_to_zero(object builder)
    {
        return ((LoanBuilder)builder).WithBaseInterestRate(base_interest_rate_value)
            .WithMarginInterestRate(-base_interest_rate_value);
    }
    
    private static object creating_a_loan(object builder)
    {
        return ((LoanBuilder)builder).Build().Match();
    }
    
    private static string WhenValidating(this object previousResult, Func<object, object> func)
    {
        return previousResult.WhenValidatingTheEntity<Loan>(func);
    }
}