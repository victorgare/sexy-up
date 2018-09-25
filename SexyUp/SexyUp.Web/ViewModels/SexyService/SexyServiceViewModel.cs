using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using X.PagedList;

namespace SexyUp.Web.ViewModels.SexyService
{
    public class SexyServiceViewModel
    {
        [Required]
        public string SearchTerm { get; set; }
        public string LastSearchTerm { get; set; }

        public IPagedList<ApplicationCore.Entities.SexyService> SearchResult { get; set; }
        public int Page { get; set; }
    }
}