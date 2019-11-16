using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.Models
{
    internal interface ITransfer
    {
        string CardNumberTo { get; set; }
        Account AccountFrom { get; set; }
        decimal Amount { get; set; }

        void DoTransfer();
    }
}
