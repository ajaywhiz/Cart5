using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CartFive.Utils
{
    public class EmailDataTypeAttribute:RegularExpressionAttribute
    {
        public EmailDataTypeAttribute():base(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        {

        }
    }
}