using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using X.PagedList;

namespace SexyUp.Web.ViewModels.SearchResult
{
    public class SearchResultViewModel
    {
        [Required]
        public string SearchTerm { get; set; }
        public string LastSearchTerm { get; set; }

        public IPagedList<ApplicationCore.Entities.Product> SearchResult { get; set; }
        public int Page { get; set; }

        public string[] CategoriesSelected { get; set; }
        public MultiSelectList CategoriesMultiSelect { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public decimal? MinPriceSelected { get; set; }
        public decimal? MaxPriceSelected { get; set; }

    }
}