using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ITransactionItensRepository
    {
        void Insert(List<TransactionItens> transactionItens);
    }
}