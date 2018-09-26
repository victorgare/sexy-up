using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Image), Schema = "dbo")]
    public class Image
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public string ImageName { get; set; }
        public string ImageOriginalName { get; set; }
        public string IdProduct { get; set; }
        public bool Cover { get; set; }

        [NotMapped]
        public string ImagePath => Path.Combine("\\Images\\products-images", ImageName);
    }
}