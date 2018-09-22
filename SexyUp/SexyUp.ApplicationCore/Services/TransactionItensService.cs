using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;

namespace SexyUp.ApplicationCore.Services
{
    public class TransactionItensService : ITransactionItensService
    {
        private readonly ITransactionItensRepository _transactionItensRepository;

        public TransactionItensService(ITransactionItensRepository transactionItensRepository)
        {
            _transactionItensRepository = transactionItensRepository;
        }

        public void Insert(List<TransactionItens> transactionItens)
        {
            _transactionItensRepository.Insert(transactionItens);
        }
    }
}