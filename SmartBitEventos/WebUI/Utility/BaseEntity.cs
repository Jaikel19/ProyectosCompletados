﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class BaseEntity
    {
       
        public String GetEntityInformation()
        {
            var dump = ObjectDumper.Dump(this);
            return dump;
        }
    }
}
