using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Booleans.Models;
using Booleans.Tools;
using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;
using Booleans.Views;

namespace Booleans.ViewModels
{
    class WelcomeViewModel : BaseViewModel
    {
        private RelayCommand<object> _backCommand;
        private RelayCommand<object> _goTransferHistoryCommand;
        private RelayCommand<object> _goTransferCommand;
        private RelayCommand<object> _goLimitCommand;
        private string _description;
        private Account _selectedAccount;
        private bool _isActiveTransfer;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                Description = "Visible";
                IsActiveTransfer = true;
                OnPropertyChanged();
            }
        }

        public bool IsActiveTransfer
        {
            get => _isActiveTransfer;
            set
            {
                _isActiveTransfer = value;
                OnPropertyChanged();
            }
        }

        public List<Account> Accounts { get; set; }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public WelcomeViewModel()
        {
            Description = "Hidden";
            IsActiveTransfer = false;
            Accounts = StationManager.DataStorage.CurrentAccounts;
        }

        public RelayCommand<Object> BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand<object>(o => NavigationManager.Instance.Navigate(ViewType.SignIn)));
            }
        }
        public RelayCommand<Object> GoTransferCommand
        {
            get
            {
                return _goTransferCommand ?? (_goTransferCommand = new RelayCommand<object>(o =>
                {
                    StationManager.DataStorage.CurrentAccount = SelectedAccount;
                    NavigationManager.Instance.Navigate(ViewType.Transfer);
                }));
            }
        }
        public RelayCommand<Object> GoLimitCommand
        {
            get
            {
                return _goLimitCommand ?? (_goLimitCommand = new RelayCommand<object>(o =>
                {
                    StationManager.DataStorage.CurrentAccount = SelectedAccount;
                    var newWindow = new LimitView();
                    newWindow.ShowDialog();
                }));
            }
        }

        public RelayCommand<Object> GoHistoryCommand
        {
            get
            {
                return _goTransferHistoryCommand ?? (_goTransferHistoryCommand = new RelayCommand<object>(o =>
                {
                    StationManager.DataStorage.CurrentAccount = SelectedAccount;
                    var newWindow = new TransferHistoryView();
                    newWindow.ShowDialog();
                }));
            }
        }
    }
}