using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
    public class UserDirections
    {
        public string idAddress { get; set; }
        public string direction { get; set; }
        
        public UserDirections()
        {
            idAddress = "";
            direction = "";
        }
    }
}
