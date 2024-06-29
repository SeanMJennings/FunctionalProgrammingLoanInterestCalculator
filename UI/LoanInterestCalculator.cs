using Application;
using Infrastructure;
using Utilities;

namespace UI;

public static class LoanInterestCalculator
{
    public static void Run()
    {
        Console.WriteLine("To end loan interest calculator, type: exit");
        while (true)
        {
            CreateOrUpdateLoan();
        }
        // ReSharper disable once FunctionNeverReturns
    }
    
    private static void CreateOrUpdateLoan()
    {
        if (LoanControllers.ListLoans().ToList().Count == 0)
        {
            CreateLoan();
        }
        else
        {
            foreach (var loanDtos in LoanControllers.ListLoans().ToList())
            {
                Console.WriteLine(loanDtos.Data);
            }
            var input = GetInput<string>("\nTo update existing loan, type: update. Else type any other key.", s => s);
            if (input.Equals("update", StringComparison.InvariantCultureIgnoreCase))
            {
                UpdateLoan();
            }
            else
            {
                CreateLoan();
            }
        }
    }

    private static void CreateLoan()
    {
        Console.WriteLine("Create loan:");
        GetLoanInputs();
        var response = LoanControllers.CreateLoan(Services.LoanDtoBuilder.BuildCreateLoanDto());
        if(!response.Success) Console.WriteLine(response.ErrorMessage());
    }
    
    private static void UpdateLoan()
    {
        Console.WriteLine("Updating loan:");
        var loanId = GetInput("Please enter the loan id", uint.Parse);
        Services.LoanDtoBuilder.WithId(loanId);
        GetLoanInputs();
        var response = LoanControllers.UpdateLoan(Services.LoanDtoBuilder.BuildUpdateLoanDto());
        if(!response.Success) Console.WriteLine(response.ErrorMessage());
    }

    private static void GetLoanInputs()
    {
        GetInput("Please enter the loan start date in format: YYYY-MM-DD", Services.LoanDtoBuilder.WithStartDate);
        GetInput("Please enter the loan end date in format: YYYY-MM-DD", Services.LoanDtoBuilder.WithEndDate);
        GetInput("Please enter the loan amount", Services.LoanDtoBuilder.WithAmount);
        Console.WriteLine($"Supported currencies: {string.Join(", ", Regions.Countries())}");
        GetInput("Please enter the currency code", Services.LoanDtoBuilder.WithCurrency);
        GetInput("Please enter the base interest rate", Services.LoanDtoBuilder.WithBaseInterestRate);
        GetInput("Please enter the margin interest rate", Services.LoanDtoBuilder.WithMarginInterestRate);
    }

    private static T GetInput<T>(string prompt, Func<string,T> func)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (input?.Equals("exit", StringComparison.InvariantCultureIgnoreCase) ?? false)
                {
                    Environment.Exit(0);
                }

                return func(input!);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException or OverflowException or ArgumentNullException or FormatException)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                    throw;
            }
        }
    }
}