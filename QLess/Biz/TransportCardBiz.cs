using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QLess.Models;
using QLess.Db;

namespace QLess.Biz
{
    public class TransportCardBiz
    {
        private readonly QLessDbContext dbContext;

        public TransportCardBiz(QLessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TransportCard BuyNew()
        {
            TransportCard transportCard = new TransportCard();
            transportCard.Id = Guid.NewGuid();
            // Q-LESS Transport Cards will have an initial load of P100
            transportCard.Balance = 100;
            transportCard.CardType = TransportCardType.Regular;
            transportCard.PurchaseDate = DateTime.Today;
            dbContext.TransportCards.Add(transportCard);
            dbContext.SaveChanges();
            return transportCard;
        }

        public decimal CheckBalance(string id)
        {
            ValidateTransportCardId(id);
            Guid guid = Guid.Parse(id);
            TransportCard transportCard = dbContext.TransportCards.FirstOrDefault(tc => tc.Id == guid);
            ValidateTransportCardNotFound(transportCard);
            ValidateTransportCardExpired(transportCard);
            return transportCard.Balance;
        }

        public decimal ReloadCard(string id, decimal loadAmount)
        {
            ValidateTransportCardId(id);
            Guid guid = Guid.Parse(id);
            TransportCard transportCard = dbContext.TransportCards.FirstOrDefault(tc => tc.Id == guid);
            ValidateTransportCardNotFound(transportCard);
            ValidateTransportCardExpired(transportCard);

            /*
                Q-LESS Transport Cardholder should be able to load their card with a starting value of P100 up to
                P10,000.
            */
            if (loadAmount < 100 || loadAmount > 10000)
            {
                throw new BizException("Load Amount must be from P100 to P10,000.");
            }

            transportCard.Balance += loadAmount;
            dbContext.SaveChanges();
            return transportCard.Balance;
        }

        public void DiscountedCardRegistration(string id, string seniorCitizenControlNumber, string pwdIdNumber)
        {
            ValidateTransportCardId(id);
            Guid guid = Guid.Parse(id);
            TransportCard transportCard = dbContext.TransportCards.FirstOrDefault(tc => tc.Id == guid);
            ValidateTransportCardNotFound(transportCard);
            ValidateTransportCardExpired(transportCard);

            if (string.IsNullOrWhiteSpace(seniorCitizenControlNumber) && string.IsNullOrWhiteSpace(pwdIdNumber))
            {
                throw new BizException("Please provide either Senior Citizen Control Number or PWD ID Number.");
            }

            /*
                The commuter would have to provide either a Senior Citizen Control Number or PWD ID Number
                together with the Q-LESS Transport Card Serial Number when registering a card.
                    o Senior Citizen Control Number is a 10-character length string with “##-####-####” format
                    o PWD ID Number is a 12-character length string with “####-####-####” format
            */
            if (!string.IsNullOrWhiteSpace(seniorCitizenControlNumber))
            {
                if (!Regex.IsMatch(seniorCitizenControlNumber, @"^\d{2}-\d{4}-\d{4}$"))
                {
                    throw new BizException("Senior Citizen Control Number must be a 10-character length string with “##-####-####” format.");
                }
            }
            if (!string.IsNullOrWhiteSpace(pwdIdNumber))
            {
                if (!Regex.IsMatch(pwdIdNumber, @"^\d{4}-\d{4}-\d{4}$"))
                {
                    throw new BizException("PWD ID Number must be a 12-character length string with “####-####-####” format.");
                }
            }

            // A Q-LESS Transport Card can only be registered once and non-reversible.
            if (transportCard.IsRegistered)
            {
                throw new BizException("Transport Card can only be registered once.");
            }

            // A Q-LESS Transport Card can be registered within 6 months upon purchase.
            DateTime registrationExpiryDate = transportCard.PurchaseDate.AddMonths(6);
            if (DateTime.Today > registrationExpiryDate)
            {
                throw new BizException("Transport Card can be registered within 6 months upon purchase.");
            }

            transportCard.CardType = TransportCardType.Discounted;
            transportCard.SeniorCitizenControlNumber = seniorCitizenControlNumber;
            transportCard.PwdIdNumber = pwdIdNumber;
            transportCard.IsRegistered = true;
            dbContext.SaveChanges();
        }

        public decimal TapIn(string id, string transportLineId, string fromStationId)
        {
            ValidateTransportCardId(id);
            Guid guid = Guid.Parse(id);
            TransportCard transportCard = dbContext.TransportCards.FirstOrDefault(tc => tc.Id == guid);
            ValidateTransportCardNotFound(transportCard);
            ValidateTransportCardExpired(transportCard);
            ValidateTransportLineNotFound(transportLineId);
            ValidateTransportStationNotFound(transportLineId, fromStationId);

            if (!string.IsNullOrWhiteSpace(transportCard.TransportLineId))
            {
                throw new BizException("Transport card already tapped-in.");
            }

            decimal minFare = dbContext.TransportFares.Where(tf => tf.TransportLineId == transportLineId).Min(tf => tf.Fare);
            if (transportCard.Balance < minFare)
            {
                throw new BizException("Not enough balance. Please top up.");
            }

            transportCard.TransportLineId = transportLineId;
            transportCard.FromStationId = fromStationId;
            transportCard.LastTapInDate = DateTime.Today;
            dbContext.SaveChanges();
            return transportCard.Balance;
        }

        public decimal TapOut(string id, string transportLineId, string toStationId)
        {
            ValidateTransportCardId(id);
            Guid guid = Guid.Parse(id);
            TransportCard transportCard = dbContext.TransportCards.FirstOrDefault(tc => tc.Id == guid);
            ValidateTransportCardNotFound(transportCard);
            ValidateTransportCardExpired(transportCard);
            ValidateTransportLineNotFound(transportLineId);
            ValidateTransportStationNotFound(transportLineId, toStationId);

            if (string.IsNullOrWhiteSpace(transportCard.TransportLineId))
            {
                throw new BizException("Transport card not tapped-in.");
            }

            if (transportCard.TransportLineId != transportLineId)
            {
                throw new BizException("Transport card tapped-in on a different line.");
            }

            if (transportCard.LastTapOutDate == DateTime.Today)
            {
                transportCard.SameDayUseCount++;
            }
            else
            {
                transportCard.SameDayUseCount = 1;
            }
            transportCard.Balance = CalculateNewCardBalance(transportCard, toStationId);
            transportCard.TransportLineId = "";
            transportCard.FromStationId = "";
            transportCard.LastTapOutDate = DateTime.Today;
            dbContext.SaveChanges();
            return transportCard.Balance;
        }

        private decimal CalculateNewCardBalance(TransportCard transportCard, string toStationId)
        {
            TransportFare transportFare = dbContext.TransportFares.FirstOrDefault(tf => tf.TransportLineId == transportCard.TransportLineId && tf.FromStationId == transportCard.FromStationId && tf.ToStationId == toStationId);

            if (transportFare == null)
            {
                throw new BizException("Transport fare record not found.");
            }

            decimal discount = 0;

            // Discounted Card Types should apply 20% discounts.
            if (transportCard.CardType == TransportCardType.Discounted)
            {
                discount += 0.2m;
            }

            /*
                For every additional use of the transport system using the Q-LESS Transport card, an additional
                3% discount will be applied with a maximum of 4 discounts applied for the day. This applies to all
                card types.
            */
            /*
                When the maximum number of discounts use has been reached, regular rates for the card type
                are applied.
            */
            if (transportCard.SameDayUseCount <= 4)
            {
                discount += 0.03m;
            }

            decimal fare = transportFare.Fare;
            fare -= fare * discount;
            decimal newCardBalance = transportCard.Balance - fare;

            if (newCardBalance < 0)
            {
                throw new BizException("Not enough balance. Please top up.");
            }

            return newCardBalance;
        }

        private void ValidateTransportCardId(string id)
        {
            if (!Guid.TryParse(id, out Guid dummy))
            {
                throw new BizException("Card ID invalid.");
            }
        }

        private void ValidateTransportCardNotFound(TransportCard transportCard)
        {
            if (transportCard == null)
            {
                throw new BizException("Transport card record not found.");
            }
        }

        private void ValidateTransportCardExpired(TransportCard transportCard)
        {
            if (transportCard.LastTapInDate != null)
            {
                DateTime lastUsedDate = (DateTime)transportCard.LastTapInDate;
                // Q-LESS Transport Card is valid up to 5 years after the last used date
                DateTime expiryDate = lastUsedDate.AddYears(5);
                if (DateTime.Today > expiryDate)
                {
                    throw new BizException("Transport card expired.");
                }
            }
        }

        private void ValidateTransportLineNotFound(string transportLineId)
        {
            TransportLine transportLine = dbContext.TransportLines.FirstOrDefault(tl => tl.Id == transportLineId);
            if (transportLine == null)
            {
                throw new BizException("Transport line record not found.");
            }
        }

        private void ValidateTransportStationNotFound(string transportLineId, string transportStationId)
        {
            TransportStation transportStation = dbContext.TransportStations.FirstOrDefault(ts => ts.TransportLineId == transportLineId && ts.Id == transportStationId);
            if (transportStation == null)
            {
                throw new BizException("Transport station record not found.");
            }
        }
    }
}
