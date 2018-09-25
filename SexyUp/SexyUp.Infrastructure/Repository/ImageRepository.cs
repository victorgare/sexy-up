using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SexyUp.Infrastructure.Repository
{
    public class ImageRepository : IImageRepository
    {
        public void Insert(List<Image> images)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Image.AddRange(images);
                context.SaveChanges();
            }
        }

        public Image Find(Expression<Func<Image, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Image.FirstOrDefault(predicate);
            }
        }

        public List<Image> Where(Expression<Func<Image, bool>> predicate)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                return context.Image.Where(predicate).ToList();
            }
        }

        public void Delete(Image image)
        {
            using (var context = new ApplicationDatabaseContext())
            {
                context.Image.Remove(image);
                context.SaveChanges();
            }
        }
    }
}