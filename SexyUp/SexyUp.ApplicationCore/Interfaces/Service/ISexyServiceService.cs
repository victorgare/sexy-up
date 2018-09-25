using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface ISexyServiceService
    {
        void Insert(SexyService sexyService);
        void Update(SexyService sexyService);
        SexyService GetById(string id);
        List<SexyService> GetAll();
        List<SexyService> SearchTerm(string termToSearch, int page, int pageItens, out int totalItens);
    }
}