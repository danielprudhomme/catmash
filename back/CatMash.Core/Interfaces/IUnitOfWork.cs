﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
