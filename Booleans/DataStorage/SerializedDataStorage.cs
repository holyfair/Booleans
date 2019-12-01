using System.Collections.Generic;
using System.Linq;
using Booleans.Models;

namespace Booleans.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        public Card CurrentCard { get; set; }
        public Client CurrentClient { get; set; }
        public List<Account> CurrentAccounts { get; set; } = new List<Account>();
        public Account CurrentAccount { get ; set ; }

        public List<Transfer> CurrentHistory { get; set; } = new List<Transfer>();

        public Account GetAccountByAccountNumber(string accountName)
        {

            return CurrentAccounts.Find(item => item.AccountCard.AccountNumber == accountName);
        }
    }
}
