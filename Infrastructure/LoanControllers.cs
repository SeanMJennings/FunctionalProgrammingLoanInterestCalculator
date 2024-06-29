using Application;
using Domain.Entities;
using Persistence;

namespace Infrastructure;

public static class LoanControllers
{
    public static ResponseDto<Loan> CreateLoan(CreateLoanDto loanDto)
    {
        return LoanFunctions.CreateLoan(loanDto, Repository.NextId, Repository.SaveLoan);
    }
    
    public static IEnumerable<ResponseDto<Loan>> ListLoans()
    {
        return LoanFunctions.ListLoans(Repository.GetLoans);
    }
    
    public static ResponseDto<Loan> UpdateLoan(UpdateLoanDto loanDto)
    {
        return LoanFunctions.UpdateLoan(loanDto, Repository.UpdateLoan);
    }
}