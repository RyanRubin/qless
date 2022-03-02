using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLess.Models;
using QLess.Db;

namespace QLess.Biz
{
    public class TransportStationBiz
    {
        private readonly QLessDbContext dbContext;

        public TransportStationBiz(QLessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TransportStation> GetFromTransportLineId(string transportLineId)
        {
            return dbContext.TransportStations.Where(ts => ts.TransportLineId == transportLineId).OrderBy(ts => ts.Name);
        }
    }
}
