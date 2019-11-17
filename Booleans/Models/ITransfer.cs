using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.Models
{
    internal interface ITransfer
    {
        string CardNumberTo { get; }
        Account AccountFrom { get; }
        decimal Amount { get; }
        string PaymentType { get; }

        void DoTransfer();
    }
}
