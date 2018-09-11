using System.ComponentModel.DataAnnotations.Schema;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Product), Schema = "dbo")]
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Weight { get; set; }
        public decimal? BoxHeight { get; set; }
        public decimal? BoxWidth { get; set; }
        public decimal? BoxDepth { get; set; }
        public decimal? Unit { get; set; }
        public string Measure { get; set; }

        [Column("Status")]
        public int ProductStatus { get; set; }
        public string Brand { get; set; }
        public string Store { get; set; }
        public string Category { get; set; }
    }
}