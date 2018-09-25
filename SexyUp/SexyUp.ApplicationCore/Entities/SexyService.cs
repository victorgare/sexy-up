using System.ComponentModel.DataAnnotations.Schema;

namespace SexyUp.ApplicationCore.Entities
{
    [Table("Service", Schema = "dbo")]
    public class SexyService
    {
        public string Id { get; set; }
        public string NameService { get; set; }
        public string Description { get; set; }

        public string IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}