using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public static class UserDirectionsDAO
    {
        public static List<UserDirections> getDirections(string idUser)
        {
            string sql = "select idAddress, address from ADDRESS where idUser = " + idUser;

            DataTable dt = Connection.makeQuery(sql);

            List<UserDirections> myList = new List<UserDirections>();
            foreach (DataRow row in dt.Rows)
            {
                UserDirections u = new UserDirections();

                u.idAddress = row[0].ToString();
                u.direction = row[1].ToString();

                myList.Add(u);
            }
            return myList;
        }
        public static void createDirection(User user,string address)
        {
            string sql = string.Format(
                "insert into ADDRESS(idUser,address) " +
                "values({0}, '{1}');", user.idUser,address);

            Connection.makeAction(sql);
        }
        public static void modify(string idAddress, string address)
        {
            string sql = string.Format(
                "update ADDRESS set address = '{0}' where idAddress = {1}", address, idAddress);

            Connection.makeAction(sql);

        }
        public static void dropDirection(string idAddress)
        {
            string sql = string.Format(
             "delete from ADDRESS where idAddress={0};",
             idAddress);

            Connection.makeAction(sql);
        }

    }
}
