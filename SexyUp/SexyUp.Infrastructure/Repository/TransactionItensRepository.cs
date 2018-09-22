using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System.Collections.Generic;

namespace SexyUp.Infrastructure.Repository
{
    public class TransactionItensRepository : ITransactionItensRepository
    {
        public void Insert(List<TransactionItens> transactionItens)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.TransactionItens.AddRange(transactionItens);
                context.SaveChanges();
            }
        }
    }
}