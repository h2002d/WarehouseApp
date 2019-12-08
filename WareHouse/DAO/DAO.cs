using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WareHouse.DAO
{
    public class DAO
    {
        public static string Main_Connectionstring = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
    }
}