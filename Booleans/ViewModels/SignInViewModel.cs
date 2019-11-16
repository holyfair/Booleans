using Booleans.Tools;
using Booleans.Tools.Managers;
using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;
using Booleans.Exceptions;
using Booleans.Models;
using Booleans.Tools.Navigation;

namespace Booleans.ViewModels
{
    internal class SignInViewModel : BaseViewModel
    {
        #region Fields
        private bool _isLogInEnabled;

        #region Commands
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties


        public PasswordBox Pin { get; private set; }
        public TextBox CardNumber { get; private set; }

        public bool IsLogInEnabled
        {
            get => _isLogInEnabled;
            private set
            {
                _isLogInEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public SignInViewModel(PasswordBox box, TextBox cardNumber)
        {
            Pin = box;
            CardNumber = cardNumber;
            IsLogInEnabled = false;
            Pin.PasswordChanged += (sender, args) => { IsLogInEnabled = Vaild(); };
            CardNumber.TextChanged += (sender, args) => { IsLogInEnabled = Vaild(); };
        }

        #region Commands

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(
                           o =>
                           {
                               string sql = $"SELECT \"Pin\", \"ExpireDate\", \"Limit\", \"AccountNumber\" FROM \"Card\" WHERE \"CardNumber\"=@cardnumber";

                               using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
                               {
                                   command.Parameters.AddWithValue("@cardnumber", CardNumber.Text.Replace(" ", ""));
                                   using (NpgsqlDataReader reader = command.ExecuteReader())
                                   {
                                       if (reader.HasRows)
                                       {
                                           reader.Read();
                                           var pin = reader.GetString(0);
                                           var expireDate = reader.GetDateTime(1);
                                           var limit = reader.GetDecimal(2);
                                           var accountNumber = reader.GetString(3);

                                           if (HashManager.VerifyHashedPassword(pin, Pin.Password))
                                           {
                                               StationManager.DataStorage.CurrentCard = new Card(CardNumber.Text.Replace(" ", ""), expireDate, limit, pin, accountNumber);
                                           }
                                           else
                                           {
                                               MessageBox.Show("Not valid pin!");
                                               return;
                                           }
                                       }
                                       else
                                       {
                                           MessageBox.Show("Not valid card!");
                                           return;
                                       }
                                   }

                               }
                               GenerateCurrentClient();
                               GenerateCurrentAccounts();
                               Console.WriteLine(StationManager.DataStorage.CurrentAccounts.Count);
                               NavigationManager.Instance.Navigate(ViewType.Welcome);
                           }));
            }
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => StationManager.CloseApp()));
            }
        }

        private void GenerateCurrentAccounts()
        {
            string sql = $"SELECT \"Account\".\"AccountNumber\", \"Balance\", \"Category\", \"PiggyBank\", \"ClientId\"," +
                         $" \"Pin\", \"ExpireDate\", \"Limit\", \"CardNumber\"" +
                         $" FROM \"Account\" JOIN \"Card\" ON" +
                         $" \"Account\".\"AccountNumber\" = \"Card\".\"AccountNumber\" WHERE \"ClientId\"=@clientId";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@clientId", StationManager.DataStorage.CurrentClient.ClientId);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var accountNumber = reader.GetString(0);
                        var balance = reader.GetDecimal(1);
                        var category = reader.GetString(2);
                        var piggyBank = reader.GetDecimal(3);
                        var clientId = reader.GetString(4);
                        var pin = reader.GetString(5);
                        var expireDate = reader.GetDateTime(6);
                        var limit = reader.GetDecimal(7);
                        var cardNumber = reader.GetString(8);

                        StationManager.DataStorage.CurrentAccounts.Add(
                            new Account(balance, category, clientId, piggyBank,
                                new Card(cardNumber, expireDate, limit, pin, accountNumber)));

                    }
                }

            }
        }

        private void GenerateCurrentClient()
        {
            string sql = $"SELECT \"Client\".\"ClientId\", \"PassportNumber\", \"Surname\", \"Name\" FROM \"Client\" JOIN \"Account\" ON  \"Client\".\"ClientId\" = " +
                         $"\"Account\".\"ClientId\"  WHERE \"Account\".\"AccountNumber\" = @accountnumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@accountnumber", StationManager.DataStorage.CurrentCard.AccountNumber);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var clientId = reader.GetString(0);
                        var passportNumber = reader.GetString(1);
                        var surname = reader.GetString(2);
                        var name = reader.GetString(3);

                        StationManager.DataStorage.CurrentClient = new Client(clientId, passportNumber, surname, name);
                    }
                    else
                    {
                        throw new DatabaseException("Client doesn't exist!");
                    }
                }

            }
        }

        private bool Vaild()
        {
            return Pin.Password.Length == 4 && CardNumber.Text.Replace(" ", "").Replace("_","").Length == 16;
        }

        #endregion
    }
}