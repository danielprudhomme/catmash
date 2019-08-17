using CatMash.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatMashContext _ctx;

        public UnitOfWork(CatMashContext context)
        {
            _ctx = context;
        }

        public async Task Save()
        {
            _ctx.SaveChanges();
        }
    }
}
