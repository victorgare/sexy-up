using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
    }
}