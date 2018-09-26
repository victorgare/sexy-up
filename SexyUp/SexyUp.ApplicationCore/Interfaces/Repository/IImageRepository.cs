using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Repository
{
    public interface IImageRepository
    {
        void Insert(List<Image> images);
        Image Find(Expression<Func<Image, bool>> predicate);
        List<Image> Where(Expression<Func<Image, bool>> predicate);
        void Delete(Image image);
    }
}