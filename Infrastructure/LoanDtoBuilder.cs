using Application;
using NodaTime;
using Utilities;

namespace Infrastructure;

public class LoanDtoBuilder()
{
    private uint Id;
    private LocalDate StartDate;
    private LocalDate EndDate;
    private decimal Amount;
    private string Currency = null!;
    private decimal BaseInterestRate;
    private decimal MarginInterestRate;

    public LoanDtoBuilder WithId(uint id)
    {
        Id = id;
        return this;
    }
    
    public LoanDtoBuilder WithStartDate(string startDate)
    {
        StartDate = Date.ParseDate(startDate);
        return this;
    }
    
    public LoanDtoBuilder WithEndDate(string endDate)
    {
        EndDate = Date.ParseDate(endDate);
        return this;
    }
    
    public LoanDtoBuilder WithAmount(string amount)
    {
        Amount = decimal.Parse(amount);
        return this;
    }
    
    public LoanDtoBuilder WithCurrency(string currency)
    {
        Currency = currency;
        return this;
    }
    
    public LoanDtoBuilder WithBaseInterestRate(string baseInterestRate)
    {
        BaseInterestRate = decimal.Parse(baseInterestRate);
        return this;
    }
    
    public LoanDtoBuilder WithMarginInterestRate(string marginInterestRate)
    {
        MarginInterestRate = decimal.Parse(marginInterestRate);
        return this;
    }
    
    public CreateLoanDto BuildCreateLoanDto()
    {
        return new CreateLoanDto(StartDate, EndDate, Amount, Currency, BaseInterestRate, MarginInterestRate);
    }
    
    public UpdateLoanDto BuildUpdateLoanDto()
    {
        return new UpdateLoanDto(Id, StartDate, EndDate, Amount, Currency, BaseInterestRate, MarginInterestRate);
    }
}