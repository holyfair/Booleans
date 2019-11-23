using Booleans.Models;
using Booleans.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.ViewModels
{
    internal class TransferHistoryViewModel : BaseViewModel
    {
        private RelayCommand<object> _closeCommand;
        public List<Transfer> TransferHistory { get; set; }
        private Action close;

        public TransferHistoryViewModel(Action close)
        {
            this.close = close;
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => close()));
            }
        }
    }
}
