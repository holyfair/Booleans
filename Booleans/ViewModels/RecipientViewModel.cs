using System;
using Booleans.Models;
using Booleans.Tools;

namespace Booleans.ViewModels
{
    internal class RecipientViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        private TransferDB _transfer;
        private Action _close;

        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;

        public RecipientViewModel(string name, string surname, TransferDB transfer, Action close)
        {
            _close = close;
            Name = name;
            Surname = surname;
            _transfer = transfer;
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(o => _close.Invoke()));
            }
        }

        public RelayCommand<Object> SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(o =>
                {
                    _transfer.DoTransfer();
                    _transfer.SaveTransferToDB();
                    _close.Invoke();
                }));
            }
        }
    }
}
