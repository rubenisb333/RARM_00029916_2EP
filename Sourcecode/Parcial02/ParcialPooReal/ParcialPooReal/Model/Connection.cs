using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public static class Connection
    {
        private static string connString =
        "Server=localhost;Port=5432;User Id=postgres;Password=uca;Database=parcialpoo";

        public static DataTable makeQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }
        public static void makeAction(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}
