using Application.RequestDtos;
using Domain.Entities;
using FunctionalProgrammingKit;

namespace Application;

public static class LoanFunctions
{
    public static ResponseDto<Loan> CreateLoan(LoanDto loanDto, Func<uint> getNextId, Func<Loan, Loan> saveLoan)
    {
        var loanEntity = loanDto.ToLoanEntity(getNextId()).Map(saveLoan);
        return loanEntity.Match(
            Invalid: ResponseDto<Loan>.Invalid,
            Valid: ResponseDto<Loan>.Valid);
    }
    
    public static IEnumerable<ResponseDto<Loan>> ListLoans(Func<IEnumerable<Loan>> getLoans)
    {
        return getLoans().Select(l => Entity<Loan>.Valid(l).Match(
            Invalid: ResponseDto<Loan>.Invalid,
            Valid: ResponseDto<Loan>.Valid));
    }
}