using Domain;
using FluentAssertions;
using FunctionalProgrammingKit;
namespace Testing;

internal static class TestVerbs
{
    internal static object Given(Func<object> func)
    {
        return func.Invoke();
    }

    internal static object And(this object previousResult, Func<object, object> func)
    {
        return func.Invoke(previousResult);
    }
    
    internal static void And(this object previousResult, Action<object> func)
    {
        func.Invoke(previousResult);
    }

    internal static object When(this object previousResult, Func<object, object> func)
    {
        return func.Invoke(previousResult);
    }

    internal static object Then(this object previousResult, Func<object, object> func)
    {
        return func.Invoke(previousResult);
    }    
    
    internal static void Then(this object previousResult, Action<object> func)
    {
        func.Invoke(previousResult);
    }
    
    internal static void Scenario(Action testAction)
    {
        testAction.Invoke();
    }
    
    internal static string WhenValidating(this object previousResult, Func<object, object> func)
    {
        return string.Join("", ((ValueObject<Money>)func.Invoke(previousResult)).Match().Errors[0].Message);
    }
    
    internal static void ThenInforms(this string actualMessage, string expectedMessage)
    {
        actualMessage.Should().Be(expectedMessage);
    }
}