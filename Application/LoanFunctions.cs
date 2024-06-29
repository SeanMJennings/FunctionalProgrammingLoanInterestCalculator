using Application.RequestDtos;
using Domain.Entities;
using FunctionalProgrammingKit;

namespace Application;

public static class LoanFunctions
{
    public static ResponseDto<Loan> CreateLoan(CreateLoanDto loanDto, Func<uint> getNextId, Func<Loan, Loan> saveLoan)
    {
        var loanEntity = loanDto.ToLoanEntity(getNextId()).Map(saveLoan);
        return loanEntity.Match(
            Invalid: errors => new ResponseDto<Loan>(errors),
            Valid: loan => new ResponseDto<Loan>(loan));
    }
    
    public static IEnumerable<ResponseDto<Loan>> ListLoans(Func<IEnumerable<Loan>> getLoans)
    {
        return getLoans().Select(l => Entity<Loan>.Valid(l).Match(
            Invalid: errors => new ResponseDto<Loan>(errors),
            Valid: loan => new ResponseDto<Loan>(loan)));
    }
    
    public static ResponseDto<Loan> UpdateLoan(UpdateLoanDto loanDto, Func<Loan, Loan> updateLoan)
    {
        var loanEntity = loanDto.ToLoanEntity().Map(updateLoan);
        return loanEntity.Match(
            Invalid: errors => new ResponseDto<Loan>(errors),
            Valid: loan => new ResponseDto<Loan>(loan));
    }
}