using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public class User
    {
        public int idUser { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }

        public User()
        {
            idUser = 0;
            name = "";
            userName = "";
            password = "";
            userType = false;
        }
    }
}
