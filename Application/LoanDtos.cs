using FunctionalProgrammingKit;
using NodaTime;
using Utilities;

namespace Application.RequestDtos;

public record CreateLoanDto(LocalDate StartDate, LocalDate EndDate, decimal Amount, string Currency, decimal BaseInterestRate, decimal MarginInterestRate);
public record UpdateLoanDto(uint id, LocalDate StartDate, LocalDate EndDate, decimal Amount, string Currency, decimal BaseInterestRate, decimal MarginInterestRate);

public static class LoanDtoExtensions
{
    public static Entity<Domain.Entities.Loan> ToLoanEntity(this CreateLoanDto loanDto, uint id)
    {
        return new Domain.Entities.LoanBuilder()
            .WithId(id)
            .WithStartDate(loanDto.StartDate)
            .WithEndDate(loanDto.EndDate)
            .WithAccrualDate(Date.Now.Day)
            .WithAmount(loanDto.Amount)
            .WithCurrency(loanDto.Currency)
            .WithBaseInterestRate(loanDto.BaseInterestRate)
            .WithMarginInterestRate(loanDto.MarginInterestRate)
            .Build();
    }
    
    public static Entity<Domain.Entities.Loan> ToLoanEntity(this UpdateLoanDto loanDto)
    {
        return new Domain.Entities.LoanBuilder()
            .WithId(loanDto.id)
            .WithStartDate(loanDto.StartDate)
            .WithEndDate(loanDto.EndDate)
            .WithAccrualDate(Date.Now.Day)
            .WithAmount(loanDto.Amount)
            .WithCurrency(loanDto.Currency)
            .WithBaseInterestRate(loanDto.BaseInterestRate)
            .WithMarginInterestRate(loanDto.MarginInterestRate)
            .Build();
    }
}