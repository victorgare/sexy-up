using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}