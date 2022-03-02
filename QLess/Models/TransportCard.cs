using System;

namespace QLess.Models
{
    public class TransportCard : BaseModel
    {
        public string TransportLineId { get; set; }
        public string FromStationId { get; set; }
        public DateTime? LastTapInDate { get; set; }
        public DateTime? LastTapOutDate { get; set; }
        public decimal Balance { get; set; }
        public TransportCardType CardType { get; set; }
        public int SameDayUseCount { get; set; }
        public string SeniorCitizenControlNumber { get; set; }
        public string PwdIdNumber { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
