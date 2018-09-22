using SexyUp.ApplicationCore.Constants;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace SexyUp.ApplicationCore.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionItensService _transactionItensService;

        public TransactionService(ITransactionRepository transactionRepository, ITransactionItensService transactionItensService)
        {
            _transactionRepository = transactionRepository;
            _transactionItensService = transactionItensService;
        }


        public void PlaceOrder(Transaction transaction, List<TransactionItens> transactionItens)
        {
            transaction.Status = OrderStatus.AguardandoConfirmacaoPagamento;
            transaction.DateTransaction = DateTime.Now;

            _transactionRepository.Insert(transaction);

            // adiciona o id da transaction para os itens
            transactionItens.ForEach(c => c.IdTransaction = transaction.Id);

            // insere os itens
            _transactionItensService.Insert(transactionItens);
        }

        public List<Transaction> GetUserTransactions(string userId)
        {
            return _transactionRepository.GetUsersTransaction(userId);
        }
    }
}