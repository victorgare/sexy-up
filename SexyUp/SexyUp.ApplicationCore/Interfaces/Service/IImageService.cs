using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.ApplicationCore.Interfaces.Service
{
    public interface IImageService
    {
        void Insert(List<Image> images);
        Image GetById(string imageId);
        List<Image> GetByProductId(string idProduct);
        void DeleteImage(Image image);
    }
}