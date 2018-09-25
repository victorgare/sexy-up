using SexyUp.ApplicationCore.Entities;
using System.Collections.Generic;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}