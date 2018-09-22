using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Transaction), Schema = "dbo")]
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Status { get; set; }

        [ForeignKey(nameof(User))]
        public string IdUser { get; set; }
        public string IdNf { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DateTransaction { get; set; }

        [ForeignKey("IdTransaction")]
        public virtual List<TransactionItens> TransactionItens { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}