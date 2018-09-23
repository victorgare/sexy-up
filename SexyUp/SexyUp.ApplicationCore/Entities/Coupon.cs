using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Coupon), Schema = "dbo")]
    public class Coupon
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [NotMapped]
        public bool Valid => EndDate > DateTime.Now;
    }
}