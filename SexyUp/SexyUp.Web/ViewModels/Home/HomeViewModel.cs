using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace SexyUp.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        [Required]
        public string SearchTerm { get; set; }
        public string LastSearchTerm { get; set; }

        public IPagedList<ApplicationCore.Entities.Product> SearchResult { get; set; }
        public int Page { get; set; }
    }
}