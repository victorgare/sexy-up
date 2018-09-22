using System.Collections.Generic;
using SexyUp.ApplicationCore.Entities;

namespace SexyUp.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public bool NewOrderPlaced { get; set; } = false;
        public List<Transaction> Transactions { get; set; }
    }
}