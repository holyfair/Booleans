
using System.Windows;

namespace Booleans.Models
{
    internal class Account
    {
        private decimal _balance;
        private decimal _piggyBank;
        #region Properties

        public string BalanceString => _balance + " UAH";
        public decimal BalanceDecimal => _balance;
        public string Category { get; set; }
        public string ClientId { get; set; }
        public Card AccountCard { get; set; }

        public string PiggyBank => _piggyBank + " UAH";


        public string CardNumber
        {
            get
            {
                int len = AccountCard.CardNumber.Length;
                return '*' + AccountCard.CardNumber.Substring(len - 4, 4);
            }
        }

        #endregion


        #region Constructor
        public Account(decimal balance, string category, string clientId, decimal piggyBank, Card card)
        {
            _balance = balance;
            Category = category;
            ClientId = clientId;
            _piggyBank = piggyBank;
            AccountCard = card;
        }
        #endregion

        public override string ToString()
        {
            return $"{nameof(Balance)}: {Balance}, {nameof(Category)}: {Category}," +
                   $" {nameof(ClientId)}: {ClientId}, {nameof(AccountCard)}: {AccountCard}";
        }
    }
}
