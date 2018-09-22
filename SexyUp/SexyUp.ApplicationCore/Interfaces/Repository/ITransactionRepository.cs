using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        void Insert(Transaction transaction);
        List<Transaction> GetUsersTransaction(string userId);
        Transaction Find(Expression<Func<Transaction, bool>> predicate);
    }
}