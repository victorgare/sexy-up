using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface ITransactionItensService
    {
        void Insert(List<TransactionItens> transactionItens);
    }
}