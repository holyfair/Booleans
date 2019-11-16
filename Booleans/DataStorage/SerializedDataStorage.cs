

using System.Collections.Generic;
using Booleans.Models;

namespace Booleans.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        public Card CurrentCard { get; set; }
        public Client CurrentClient { get; set; }
        public List<Account> CurrentAccounts { get; set; } = new List<Account>();
        public Account CurrentAccount { get ; set ; }
    }
}
