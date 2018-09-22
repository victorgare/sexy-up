using System;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SexyUp.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public void Insert(Transaction transaction)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Transaction.Add(transaction);
                context.SaveChanges();
            }
        }

        public List<Transaction> GetUsersTransaction(string userId)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Transaction.Where(c => c.IdUser.Equals(userId))
                    .Include(c => c.TransactionItens)
                    .ToList();
            }
        }

        public Transaction Find(Expression<Func<Transaction, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Transaction
                    .Include(c => c.User)
                    .Include(c => c.TransactionItens)
                    .Include(c => c.TransactionItens.Select(d => d.Product))
                    .Include(c => c.TransactionItens.Select(d => d.Product.Image))
                    .FirstOrDefault(predicate);
            }
        }
    }
}