using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface ITransactionService
    {
        void PlaceOrder(Transaction transaction, List<TransactionItens> transactionItens);
        List<Transaction> GetUserTransactions(string userId);
    }
}