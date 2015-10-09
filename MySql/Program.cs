using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hassium;
using Hassium.Functions;
using Hassium.HassiumObjects;

namespace MySqlLib
{
    public class Program : ILibrary
    {
		[IntFunc("Sql", true, 4)]
        public static HassiumObject Sql(HassiumObject[] args)
        {
            return new HassiumSql(args[0].ToString(), args[1].ToString(), args[2].ToString(), args[3].ToString());
        }
    }
}

