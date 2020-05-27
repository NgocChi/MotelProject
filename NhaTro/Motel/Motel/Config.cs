using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel
{
    public static class Config
    {
        public static string SubFolder { set; get; }

        public static void LoadConfig(IConfiguration configuration)
        {
            SubFolder = (string)configuration.GetValue(typeof(string), "Subfolder");
        }
    }
}
