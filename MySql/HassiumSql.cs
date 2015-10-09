using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace MySqlLib
{
    public class HassiumSql: HassiumObject
    {
        public MySqlConnection Value { get; private set; }

        public HassiumSql(string server, string database, string user, string password)
        {
            Value = new MySqlConnection("Server=" + server + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";Pooling=false");
            Attributes.Add("open", new InternalFunction(open, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("query", new InternalFunction(query, 3));
            Attributes.Add("select", new InternalFunction(select, -1));
        }

        private HassiumObject open(HassiumObject[] args)
        {
            Value.Open();

            return null;
        }

        private HassiumObject query(HassiumObject[] args)
        {
            MySqlCommand command = new MySqlCommand(null, Value);
            command.CommandText = "INSERT INTO " + args[0] + "(";
            HassiumObject[] vals = ((HassiumArray)args[1]).Value;
            command.CommandText += vals[0].ToString();
            for (int x = 1; x < vals.Length; x++)
                command.CommandText += ", " + vals[x].ToString();
            command.CommandText += ") VALUES (";
	    command.CommandText += "@" + vals[0].ToString();
	    for (int x = 1; x < vals.Length; x++)
		command.CommandText += ", @" + vals[x].ToString();
	    command.CommandText += ")";

	    HassiumObject[] lits = ((HassiumArray)args[2]).Value;
	    for (int x = 0; x < lits.Length; x++) {
		MySqlParameter param = new MySqlParameter("@" + vals[x].ToString(), lits[x].ToString());
		param.Value = lits[x].ToString();
		command.Parameters.Add(param);
	    }
	    // Console.WriteLine(command.CommandText);



	    command.Prepare();
            command.ExecuteNonQuery();

            return null;
        }

        private HassiumObject select(HassiumObject[] args)
        {
		MySqlCommand command = new MySqlCommand(args[0].ToString(), Value);
		for (int x = 1; x < args.Length; x++)
			command.Parameters.AddWithValue(((HassiumArray)args[x]).Value[0].ToString(), ((HassiumArray)args[x]).Value[1].ToString());
		command.Prepare();

		return new HassiumSqlDataReader(command.ExecuteReader());
	}

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();

            return null;
        }
    }
}

