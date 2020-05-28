using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public static class ProductDAO
    {
        public static List<Product> getAll()
        {
            string sql = "select * from PRODUCT";

            DataTable dt = Connection.makeQuery(sql);

            List<Product> myList = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                Product u = new Product();

                u.idProduct = Convert.ToInt32(row[0].ToString());
                u.businessId = Convert.ToInt32(row[1].ToString());
                u.productName = row[2].ToString();

                myList.Add(u);
            }
            return myList;
        }
        public static void create(Product product)
        {
            string sql = string.Format(
            "insert into PRODUCT(idBusiness,name) " +
            "values({0}, '{1}');", product.businessId, product.productName);

            Connection.makeAction(sql);
        }
    }
}
