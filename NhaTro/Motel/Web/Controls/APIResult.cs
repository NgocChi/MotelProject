using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controls
{
    public class ApiResult
    {
        public object Data { get; set; }
        public int Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
