using System.Collections.Generic;
using System.Windows.Documents;
using Booleans.Models;

namespace Booleans.DataStorage
{
    internal interface IDataStorage
    {
        Card CurrentCard { get; set; }
        List<Account> CurrentAccounts { get; set; }
        Client CurrentClient { get; set; }
        Account CurrentAccount { get; set; }
    }
}
