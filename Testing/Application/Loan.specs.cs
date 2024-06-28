using NUnit.Framework;

namespace Testing.Application;

public static partial class LoanSpecsShould
{
    [Test]
    public static void create_a_loan()
    {
        Given(a_valid_loan_dto)
            .When(creating_a_loan)
            .Then(the_loan_is_listed);
    }
}