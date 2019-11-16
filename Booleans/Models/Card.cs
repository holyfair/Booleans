using System;

namespace Booleans.Models
{
    internal class Card
    {
        #region Properties
        public string CardNumber { get; set; } 
        public DateTime ExpireDate { get; set; }
        public decimal Limit { get; set; }
        public string Pin { get; set; }

        public string AccountNumber { get; set; }
        #endregion

        #region Constructor
        internal Card(string cardNumber, DateTime expireDate, decimal limit, string pin, string accountNumber)
        {
            CardNumber = cardNumber;
            ExpireDate = expireDate;
            Limit = limit;
            Pin = pin;
            AccountNumber = accountNumber;
        }

        public Card(string accountNumber)
        {
            AccountNumber = accountNumber;
        }
        #endregion

        public override string ToString()
        {
            return $"{nameof(CardNumber)}: {CardNumber}, {nameof(ExpireDate)}: {ExpireDate}, {nameof(Limit)}: {Limit}, {nameof(Pin)}: {Pin}, {nameof(AccountNumber)}: {AccountNumber}";
        }
    }


}
