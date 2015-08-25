using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hassium;

namespace MySqlLib
{
    public class Program : ILibrary
    {
		[IntFunc("newsql")]
        public static object NewSql(object[] args)
        {
            return new MySqlConnection("Server=" + args[0].ToString() + ";Database=" + args[1].ToString() + ";User ID=" + args[2].ToString() + ";Password=" + args[3].ToString() + ";Pooling=false");
        }

		[IntFunc("sqlopen")]
        public static object SqlOpen(object[] args)
        {
            ((MySqlConnection)args[0]).Open();
            return null;
        }

		[IntFunc("sqlclose")]
        public static object SqlClose(object[] args)
        {
            ((MySqlConnection)args[0]).Close();
            return null;
        }

		[IntFunc("sqlquery")]
        public static object SqlQuery(object[] args)
        {
            MySqlCommand cmd = new MySqlCommand(args[1].ToString(), ((MySqlConnection)args[0]));
	    cmd.Prepare();
            cmd.ExecuteNonQuery();
            return null;
        }

		[IntFunc("sqlselect")]
        public static object SqlSelect(object[] args)
        {
            string[] result = new string[100];
            MySqlCommand cmd = new MySqlCommand("SELECT " + args[1].ToString() + " FROM " + args[2].ToString(), ((MySqlConnection)args[0]));
	    cmd.Prepare();

            MySqlDataReader dataReader = cmd.ExecuteReader();

            for (int x = 0; dataReader.Read(); x++)
            {
                result[x] = dataReader[args[3].ToString()] + "";
            }

            return result;
        }
    }
}

