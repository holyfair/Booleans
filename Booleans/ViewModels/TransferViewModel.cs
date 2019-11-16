using System;
using Booleans.Tools;
using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;

namespace Booleans.ViewModels
{
    class TransferViewModel: BaseViewModel
    {
        private string _accountNumber;
        private decimal _amount;
        private string _accountType;
        private string _frequency;

        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _acceptCommand;

        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                OnPropertyChanged();
            }
        }

        public string Frequency
        {
            get => _frequency;
            set
            {
                _frequency = value;
                OnPropertyChanged();
            }
        }

        public string AccountType
        {
            get => _accountType;
            set
            {
                _accountNumber = value;
                OnPropertyChanged();
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.Welcome)));
            }
        }

        public RelayCommand<Object> AcceptCommand
        {
            get
            {
                return _acceptCommand ?? (_acceptCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.Welcome)));
            }
        }

        private void 
    }
}
