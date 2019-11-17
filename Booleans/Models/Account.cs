
using System.Windows;
using Booleans.Tools;

namespace Booleans.Models
{
    internal class Account : BaseViewModel
    {
        private decimal _balance;
        private decimal _piggyBank;
        #region Properties

        public string BalanceString => _balance + " UAH";

        public decimal BalanceDecimal
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            } 
        }
        public string Category { get; set; }
        public string ClientId { get; set; }
        public Card AccountCard { get; set; }

        public string PiggyBankString => _piggyBank + " UAH";
        public decimal PiggyBankDecimal
        {
            get => _piggyBank;
            set
            {
                _piggyBank = value;
                OnPropertyChanged();
            }
        }


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

        public Account(string accountNumber, decimal balance, decimal piggyBank)
            :this(balance, "", "", piggyBank, new Card(accountNumber))
        {
        }
        #endregion

        public override string ToString()
        {
            return $"{nameof(BalanceDecimal)}: {BalanceDecimal}, {nameof(Category)}: {Category}," +
                   $" {nameof(ClientId)}: {ClientId}, {nameof(AccountCard)}: {AccountCard}";
        }
    }
}
