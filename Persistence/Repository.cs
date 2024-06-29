using System.Collections.Immutable;
using Domain.Entities;

namespace Persistence;

public static class Repository
{
    internal static readonly List<Loan> _loans = [];
    
    public static Func<Loan, Loan> SaveLoan => loan =>
    {
        _loans.Add(loan);
        return loan;
    };
    
    public static Func<Loan, Loan> UpdateLoan => loan =>
    {
        var existingLoan = _loans.First(l => l.Id == loan.Id);
        _loans.Remove(existingLoan);
        _loans.Add(loan);
        return loan;
    };

    public static IEnumerable<Loan> GetLoans() => _loans.ToImmutableArray();
    
    public static Func<uint> NextId => () => (uint)_loans.Count + 1;
}