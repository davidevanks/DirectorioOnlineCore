﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PsqlConfiguration
    {
        public string ConString { get; set; }
        public PsqlConfiguration(string conString) => ConString = conString;



    }
}
