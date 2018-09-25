using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EntityValidation.Attributes;
using EntityValidation.Validation;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Product), Schema = "dbo")]
    public class Product : Validate<Product>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Mandatory("O {0} é obrigatório")]
        public string Name { get; set; }

        [Mandatory("A {0} é obrigatória")]
        public string Description { get; set; }

        [Mandatory("O {0} é obrigatório")]
        public decimal Price { get; set; }
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

        [Column(nameof(Category)), Mandatory("A {0} é obrigatória")]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey("IdProduct")]
        public virtual List<Image> Image { get; set; }
    }
}