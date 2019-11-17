using System;
using System.Windows.Controls;
using Booleans.Exceptions;
using Booleans.Models;
using Booleans.Tools;
using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;
using Booleans.Views;
using Npgsql;

namespace Booleans.ViewModels
{
    class TransferViewModel: BaseViewModel
    {
        private string _cardNumber;
        private decimal _amount;
        private string _frequency;

        private RelayCommand<object> _closeCommand;
        private RelayCommand<object> _acceptCommand;

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
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

        public ComboBoxItem SelectedPaymentType { get; set; }

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
                return _acceptCommand ?? (_acceptCommand = new RelayCommand<object>(o => Accept()));
            }
        }

        private Client getClientTo()
        {
            string sql = $"SELECT \"Surname\", \"Name\" FROM (\"Client\" JOIN \"Account\" ON  \"Client\".\"ClientId\" = " +
                         $"\"Account\".\"ClientId\") JOIN \"Card\" ON \"Card\".\"AccountNumber\" = \"Account\".\"AccountNumber\"" +
                         $" WHERE \"Card\".\"CardNumber\" = @cardNumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@cardNumber", CardNumber.Replace(" ", ""));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var surname = reader.GetString(0);
                        var name = reader.GetString(1);

                        return new Client(surname, name);
                    }
                    else
                    {
                        throw new DatabaseException("Client doesn't exist!");
                    }
                }

            }
        }

        private void Accept()
        {
            TransferDB transfer = new TransferDB(CardNumber, StationManager.DataStorage.CurrentAccount, Amount, SelectedPaymentType.Content.ToString());
            var newWindow = new RecipientView(transfer, getClientTo());
            newWindow.ShowDialog();
        }
    }
}
