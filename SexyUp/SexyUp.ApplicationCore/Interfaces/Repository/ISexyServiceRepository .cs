using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ISexyServiceRepository
    {
        void Insert(SexyService sexyService);
        void Update(SexyService sexyService);
        SexyService GetById(string id);
        List<SexyService> GetAll();
        List<SexyService> SearchTerms(string[] terms, int page, int pageItens, out int totalItens);
    }
}