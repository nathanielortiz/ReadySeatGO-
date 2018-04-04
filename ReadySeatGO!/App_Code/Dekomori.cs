using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ReadySeatGO_.App_Code
{
    public class Dekomori
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
        }

        
    }
}
