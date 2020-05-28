using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public static class BusinessDAO
    {
        public static List<Business> getBusiness()
        {
            string sql = "select * from BUSINESS";

            DataTable dt = Connection.makeQuery(sql);

            List<Business> myList = new List<Business>();
            foreach (DataRow row in dt.Rows)
            {
                Business u = new Business();

                u.idBusiness = Convert.ToInt32(row[0].ToString());
                u.name = row[1].ToString();
                u.description = row[2].ToString();

                myList.Add(u);
            }
            return myList;
        }
        public static void createBusiness(Business business)
        {
            string sql = string.Format(
            "insert into BUSINESS(name,description) " +
            "values('{0}', '{1}');", business.name, business.description);

            Connection.makeAction(sql);
        }

        public static void modify(Business business)
        {
            string sql = string.Format(
                "update BUSINESS set name = '{0}', description = '{1}' where idbusiness = {2}",
                business.name, business.description, business.idBusiness);

            Connection.makeAction(sql);

        }
        public static void drop(string id)
        {
            string sql = string.Format(
             "delete from BUSINESS where idbusiness={0};",
             id);

            Connection.makeAction(sql);
        }

    }
}
