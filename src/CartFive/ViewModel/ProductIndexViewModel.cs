using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CartFive.Models;
using CartFive.Utils;

namespace CartFive.ViewModel
{
    public class ProductIndexViewModel : IPagingProperties
    {
        public List<Product> Products { get; set; }        

        #region IPagingProperties Members

        public int TotalRecords { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string QueryString { get; set; }

        #endregion
    }
}