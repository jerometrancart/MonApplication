﻿using SelfieAWookie.API.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Domain
{
    public interface ISelfieRepository
    {
        ICollection<Selfie> GetAll();
    }
}
