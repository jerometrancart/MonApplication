﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfiesAWookie.Core.Framework
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
