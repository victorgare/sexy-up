using System.ComponentModel.DataAnnotations.Schema;

namespace SexyUp.ApplicationCore.Entities
{
    [Table(nameof(Category), Schema = "dbo")]
    public class Category
    {
        public string Id { get; set; }

        [Column(nameof(Category))]
        public string Name { get; set; }
    }
}