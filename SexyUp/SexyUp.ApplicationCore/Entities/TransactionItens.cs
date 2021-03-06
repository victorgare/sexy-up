﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(TransactionItens), Schema = "dbo")]
    public class TransactionItens
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string IdTransaction { get; set; }

        [ForeignKey(nameof(Product))]
        public string IdProduct { get; set; }

        [Column("Qtd")]
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}