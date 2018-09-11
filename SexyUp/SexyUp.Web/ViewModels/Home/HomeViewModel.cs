using System.Collections.Generic;

namespace SexyUp.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public string SearchTerm { get; set; }

        public List<ApplicationCore.Entities.Product> SearchResult { get; set; }
    }
}