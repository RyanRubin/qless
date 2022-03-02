using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLess.Models;
using QLess.Db;

namespace QLess.Biz
{
    public class TransportLineBiz
    {
        private readonly QLessDbContext dbContext;

        public TransportLineBiz(QLessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TransportLine> Get()
        {
            return dbContext.TransportLines.OrderBy(tl => tl.Name);
        }
    }
}
