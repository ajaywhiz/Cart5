using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;

namespace CartFive.Utils
{
    public class JsonObjectFilter:ActionFilterAttribute
    {
        public string Param { get; set; }
        public Type RootType { get; set; }
        public string FormElement { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(FormElement))
            {
                //if request type is JSON
                if ((filterContext.HttpContext.Request.ContentType ?? string.Empty).Contains("application/json"))
                {
                    object o =
                    new DataContractJsonSerializer(RootType).ReadObject(filterContext.HttpContext.Request.InputStream);
                    filterContext.ActionParameters[Param] = o;
                }
            }
            else
            {
                //if JSON string is in a form field
                string formData = filterContext.HttpContext.Request[FormElement];
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(formData);
                MemoryStream ms = new MemoryStream(byteData);
                object o = new DataContractJsonSerializer(RootType).ReadObject(ms);
                filterContext.ActionParameters[Param] = o;

                ms.Dispose();
                ms = null;
            }

        }

    }
}