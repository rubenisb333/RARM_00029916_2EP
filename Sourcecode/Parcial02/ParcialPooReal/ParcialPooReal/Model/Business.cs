﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialPooReal.Model
{
   
    public class Business
    {
        public int idBusiness { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Business()
        {
            idBusiness = 0;
            name = "";
            description = "";
        }


    }
}
