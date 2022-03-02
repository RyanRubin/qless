using System;

namespace QLess.Models
{
    public class TransportFare : BaseModel
    {
        public string TransportLineId { get; set; }
        public string FromStationId { get; set; }
        public string ToStationId { get; set; }
        public decimal Fare { get; set; }
    }
}
