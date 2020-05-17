using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class GlobalConfiguration
    {
        public static string SQLConnectionString { set; get; } = "Server=DESKTOP-SQJRAD1;Database=NHATRO;User ID=sa;Password=585858;";


        public static void Load(IConfiguration _configuration)
        {
            SQLConnectionString = _configuration.GetConnectionString("DefaultConnectionString");
           

        }
    }
}
