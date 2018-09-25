using SexyUp.ApplicationCore.Entities;
using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.ApplicationCore.Interfaces.Service;
using System.Collections.Generic;

namespace SexyUp.ApplicationCore.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void Insert(List<Image> images)
        {
            _imageRepository.Insert(images);
        }

        public Image GetById(string imageId)
        {
            return _imageRepository.Find(c => c.Id.Equals(imageId));
        }

        public List<Image> GetByProductId(string idProduct)
        {
            return _imageRepository.Where(c => c.IdProduct.Equals(idProduct));
        }

        public void DeleteImage(Image image)
        {
            _imageRepository.Delete(image);
        }
    }
}