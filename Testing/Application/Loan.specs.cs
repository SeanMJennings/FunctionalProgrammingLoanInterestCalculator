using NUnit.Framework;

namespace Testing.Application;

public static partial class LoanSpecsShould
{
    [Test]
    public static void create_a_loan()
    {
        Given(a_valid_loan_dto).When(creating_a_loan).Then(the_created_loan_is_listed);
    }

    [Test]
    public static void update_a_loan()
    {
        Given(a_loan).When(updating_a_loan).Then(the_updated_loan_is_listed);
    }

    [Test]
    public static void lists_loans()
    {
        Given(a_loan).And(another_loan).When(listing_loans).Then(the_listed_loans_are_correct);
    }
}