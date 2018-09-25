using System;
using System.Collections.Generic;
using SexyUp.ApplicationCore.Constants;
using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;

namespace SexyUp.ApplicationCore.Services
{
    public class SexyServiceService : ISexyServiceService
    {
        private readonly ISexyServiceRepository _sexyServiceRepository;

        public SexyServiceService(ISexyServiceRepository sexyServiceRepository)
        {
            _sexyServiceRepository = sexyServiceRepository;
        }

        public void Insert(SexyService sexyService)
        {
            sexyService.Id = Guid.NewGuid().ToString();
 
            _sexyServiceRepository.Insert(sexyService);
        }

        public void Update(SexyService sexyService)
        {
            _sexyServiceRepository.Update(sexyService);  
        }

        public SexyService GetById(string id)
        {
            return _sexyServiceRepository.GetById(id);
        }

        public List<SexyService> GetAll()
        {
            return _sexyServiceRepository.GetAll();
        }

        public List<SexyService> SearchTerm(string termToSearch, int page, int pageItens, out int totalItens)
        {
            // quebra a string nos espacos para pesquisar os produtos por palavra chave
            var splitedTerms = termToSearch.ToLower().Split(null);

            return _sexyServiceRepository.SearchTerms(splitedTerms, page, pageItens, out totalItens);
        }
    }
}