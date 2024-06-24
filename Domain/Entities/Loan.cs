using Domain.Primitives;
using FunctionalProgrammingKit;
using NodaTime;
using Utilities;

namespace Domain.Entities;

public record Loan
{
    public uint Id { get; init; }
    public LocalDate StartDate { get;  init; }
    public LocalDate EndDate { get; init; }
    public int AccrualDate { get; init; }
    public Money Amount { get; init; }
    public Currency Currency { get; init; }
    public decimal BaseInterestRate { get; init; }
    public decimal MarginInterestRate { get; init; }
    
    public static Entity<Loan> Create(uint id, LocalDate startDate, LocalDate endDate, int accrualDate, decimal amount, string currency, decimal baseInterestRate, decimal marginInterestRate)
    {
        var errors = new List<Error>();
        ValidateInputs(startDate, endDate, amount, baseInterestRate, marginInterestRate, errors);
        return errors.Count > 0 ? Entity<Loan>.Invalid(errors) : Entity<Loan>.Valid(new Loan())
            .SetId(id)
            .SetStartDate(startDate)
            .SetEndDate(endDate)
            .SetAccrualDate(accrualDate)
            .SetAmount(amount)
            .SetCurrency(currency)
            .SetBaseInterestRate(baseInterestRate)
            .SetMarginInterestRate(marginInterestRate);
    }
    
    public override string ToString()
    {
        return $"""
                Loan ID: {Id}
                Period: {StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}
                Value: {Amount} {Currency}
                Base interest rate: {BaseInterestRate}
                Margin interest rate: {MarginInterestRate}
                Accrual Date: Day {AccrualDate} of month
                Daily interest without margin: {DailyInterest(Amount, BaseInterestRate, MarginInterestRate, true)} {Currency}
                Daily interest: {DailyInterest(Amount, BaseInterestRate, MarginInterestRate)} {Currency}
                Days elapsed: {DaysElapsed(StartDate)}
                Total interest: {TotalInterest(Amount, BaseInterestRate, MarginInterestRate, StartDate, EndDate)} {Currency}
                """;
    }

    private static void ValidateInputs(LocalDate startDate, LocalDate endDate, decimal amount, decimal baseInterestRate,
        decimal marginInterestRate, List<Error> errors)
    {
        if (endDate <= startDate)
        {
            errors.Add(new Error("End date must be after start date."));
        }
        if (amount <= 0)
        {
            errors.Add(new Error("Amount must be greater than zero."));
        }
        if (baseInterestRate + marginInterestRate <= 0)
        {
            errors.Add(new Error("Total interest rate must be greater than zero."));
        }
    }
}

public static class LoanFunctionality
{
    public static Money DailyInterest(Money amount, decimal baseInterestRate, decimal marginInterestRate, bool withoutMargin = false)
    {
        return amount * (withoutMargin ? baseInterestRate : baseInterestRate + marginInterestRate) / 100 / 365;
    }
    
    public static int DaysElapsed(LocalDate startDate)
    {
        return Period.Between(startDate, Date.Now, PeriodUnits.Days).Days;
    }
    
    public static Money TotalInterest(Money amount, decimal baseInterestRate, decimal marginInterestRate, LocalDate startDate, LocalDate endDate)
    {
        return DailyInterest(amount, baseInterestRate, marginInterestRate) * Period.Between(startDate, endDate, PeriodUnits.Days).Days;
    }
}

public static class LoanSetters
{
    public static Entity<Loan> SetId(this Entity<Loan> loan, uint id) 
    {
        return loan.SetValueObject(id.Create(), static (loan, identifier) => loan with { Id = identifier });
    }    
    
    public static Entity<Loan> SetStartDate(this Entity<Loan> loan, LocalDate localDate) 
    {
        return loan.SetValueObject(localDate.Create(), static (loan, the_date) => loan with { StartDate = the_date });
    }    
    
    public static Entity<Loan> SetAccrualDate(this Entity<Loan> loan, int accrualDate) 
    {
        return loan.SetValueObject(accrualDate.Create(), static (loan, the_accrual_date) => loan with { AccrualDate = the_accrual_date });
    }
    
    public static Entity<Loan> SetEndDate(this Entity<Loan> loan, LocalDate localDate) 
    {
        return loan.SetValueObject(localDate.Create(), static (loan, the_date) => loan with { EndDate = the_date });
    }
    
    public static Entity<Loan> SetAmount(this Entity<Loan> loan, decimal amount) 
    {
        return loan.SetValueObject(Money.New(amount), static (loan, the_amount) => loan with { Amount = the_amount });
    }
    
    public static Entity<Loan> SetCurrency(this Entity<Loan> loan, string currency) 
    {
        return loan.SetValueObject(Currency.New(currency), static (loan, the_currency) => loan with { Currency = the_currency });
    }
    
    public static Entity<Loan> SetBaseInterestRate(this Entity<Loan> loan, decimal baseInterestRate) 
    {
        return loan.SetValueObject(baseInterestRate.Create(), static (loan, the_rate) => loan with { BaseInterestRate = the_rate });
    }
    
    public static Entity<Loan> SetMarginInterestRate(this Entity<Loan> loan, decimal marginInterestRate) 
    {
        return loan.SetValueObject(marginInterestRate.Create(), static (loan, the_rate) => loan with { MarginInterestRate = the_rate });
    }
}

public record LoanBuilder
{
    private uint _id { get; set; } = 1;
    private LocalDate _startDate { get; set; }
    private LocalDate _endDate { get; set; }
    private int _accrualDate { get; set; }
    private decimal _amount { get; set; }
    private string _currency { get; set; } = null!;
    private decimal _baseInterestRate { get; set; }
    private decimal _marginInterestRate { get; set; }
    
    public LoanBuilder WithId(uint id)
    {
        _id = id;
        return this;
    }
    
    public LoanBuilder WithStartDate(LocalDate startDate)
    {
        _startDate = startDate;
        return this;
    }
    
    public LoanBuilder WithEndDate(LocalDate endDate)
    {
        _endDate = endDate;
        return this;
    }
    
    public LoanBuilder WithAccrualDate(int accrualDate)
    {
        _accrualDate = accrualDate;
        return this;
    }
    
    public LoanBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
    
    public LoanBuilder WithCurrency(string currency)
    {
        _currency = currency;
        return this;
    }
    
    public LoanBuilder WithBaseInterestRate(decimal baseInterestRate)
    {
        _baseInterestRate = baseInterestRate;
        return this;
    }
    
    public LoanBuilder WithMarginInterestRate(decimal marginInterestRate)
    {
        _marginInterestRate = marginInterestRate;
        return this;
    }
    
    public Entity<Loan> Build()
    {
        return Loan.Create(_id, _startDate, _endDate, _accrualDate, _amount, _currency, _baseInterestRate, _marginInterestRate);
    }
}
    