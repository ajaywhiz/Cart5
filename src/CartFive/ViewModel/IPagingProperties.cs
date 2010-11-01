using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CartFive.ViewModel
{
   public interface IPagingProperties
    {
        int TotalRecords
        {
            get;
            set;
        }

        int PageIndex
        {
            get;
            set;
        }

        int PageSize
        {
            get;
            set;
        }

        string Controller
        {
            get;
            set;
        }
        
        string Action
        {
            get;
            set;
        }
        
        string QueryString
        {
            get;
            set;
        }
    }
}