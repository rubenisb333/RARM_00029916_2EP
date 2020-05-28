using ParcialPooReal.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public static class UserDAO
    {
        public static List<User> GetUsers()
        {
            string sql = "select * from APPUSER";

            DataTable dt = Connection.makeQuery(sql);

            List<User> myList = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                User u = new User();

                u.idUser = Convert.ToInt32(row[0].ToString());
                u.name = row[1].ToString();
                u.userName = row[2].ToString();
                u.password = row[3].ToString();
                u.userType = Convert.ToBoolean(row[4].ToString());

                myList.Add(u);
            }
            return myList;
        }
        public static bool getIfPasswordMatch(string username, string passwowd)
        {
            string sql = string.Format("select username from APPUSER where username ='{0}' and password = '{1}'",username,passwowd);
            DataTable dt = Connection.makeQuery(sql);
            string baseUser = "";

            foreach (DataRow row in dt.Rows)
            {
                baseUser = row[0].ToString();
                
            }
            return  !(baseUser == "") ;
        }
        public static void createNewUser(string name, string user, string pass, bool isAdmin =false)
        {
            string sql = string.Format(
                "insert into APPUSER(fullname, username, password, userType) " +
                "values('{0}', '{1}','{2}', {3});",
                name,user, pass, isAdmin);

            Connection.makeAction(sql);
        }

        public static void dropUser(string user)
        {
            string sql = string.Format(
             "delete from appuser where username='{0}';",
             user);

            Connection.makeAction(sql);
        }

        public static void changeUserPassword(string username,  string newPass){
            string sql = string.Format(
               "update appuser set password = '{0}' where username='{1}';",
               newPass,username);

            Connection.makeAction(sql);
        }

    }
}
