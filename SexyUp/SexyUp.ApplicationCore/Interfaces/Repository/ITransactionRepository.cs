using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        void Insert(Transaction transaction);
        List<Transaction> GetUsersTransaction(string userId);
    }
}