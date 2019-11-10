
using System.Windows;

namespace Booleans.Models
{
    internal class Account
    {
        private decimal _balance;
        #region Properties

        public string Balance
        {
            get => _balance.ToString() + " грн";
        }
        public string Category { get; set; }
        public string ClientId { get; set; }
        public Card AccountCard { get; set; }


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
        public Account(decimal balance, string category, string clientId, Card card)
        {
            _balance = balance;
            Category = category;
            ClientId = clientId;
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
