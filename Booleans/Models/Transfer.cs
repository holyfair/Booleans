using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Booleans.Exceptions;
using Booleans.Tools.Managers;
using Booleans.Views;
using Npgsql;

namespace Booleans.Models
{
    internal class Transfer : ITransfer
    {
        public string CardNumberTo { get; set; }
        public Account AccountFrom { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }


        protected Account AccountTo { get; set; }

        public Transfer(string cardNumberTo, Account accountFrom, decimal amount, string paymentType)
        {
            CardNumberTo = cardNumberTo;
            AccountFrom = accountFrom;
            Amount = amount;
            PaymentType = paymentType;
            if (!IsAccountToValid())
                throw new DatabaseException("Not valid card number!");
            if (!IsValidTransaction())
                throw new DatabaseException("Not enough money or amount is bigger then limit");
        }

        public Transfer()
        {
        }

        public virtual void DoTransfer()
        {
            MakeTransition();
        }

        private void DepositMoney()
        {
            var account = StationManager.DataStorage.GetAccountByAccountNumber(AccountTo.AccountCard.AccountNumber);
            string sql = "UPDATE \"Account\" SET " +
                         "\"Balance\" = @balance, \"PiggyBank\" = @piggyBank Where \"AccountNumber\" = @accountNumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                var rest = Amount % 10;
                var newBalance = AccountTo.BalanceDecimal + (Amount - rest);

                command.Parameters.AddWithValue("@balance", newBalance);
                command.Parameters.AddWithValue("@piggyBank", AccountTo.PiggyBankDecimal + rest);
                command.Parameters.AddWithValue("@accountNumber", AccountTo.AccountCard.AccountNumber);
                if (account != null)
                {
                    account.BalanceDecimal = newBalance;
                    account.PiggyBankDecimal = rest + account.PiggyBankDecimal;
                }


                command.ExecuteNonQuery();
            }
        }

        private void WithdrawMoney(string paymentType)
        {
            string sql = "";
            decimal newBalance = 0;
            var account = StationManager.DataStorage.GetAccountByAccountNumber(AccountFrom.AccountCard.AccountNumber);
            if (paymentType == "Main")
            {
                sql = "UPDATE \"Account\" SET " +
                      "\"Balance\" = @balance Where \"AccountNumber\" = @accountNumber";
                newBalance = StationManager.DataStorage.CurrentAccount.BalanceDecimal - Amount;
                account.BalanceDecimal = newBalance;
            }
            else
            {
                sql = "UPDATE \"Account\" SET " +
                      "\"PiggyBank\" = @balance Where \"AccountNumber\" = @accountNumber";
                newBalance = StationManager.DataStorage.CurrentAccount.PiggyBankDecimal - Amount;
                account.PiggyBankDecimal = newBalance;
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@balance", newBalance);
                command.Parameters.AddWithValue("@accountNumber", AccountFrom.AccountCard.AccountNumber);

                command.ExecuteNonQuery();
            }
        }

        private async void MakeTransition()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(1000));
            WithdrawMoney(PaymentType);
            DepositMoney();
            var successful = new Successful();
            successful.ShowDialog();
            LoaderManager.Instance.HideLoader();
        }

        private bool IsAccountToValid()
        {
            string sql = $"SELECT \"Account\".\"AccountNumber\", \"Balance\", \"PiggyBank\"" +
                         $" FROM \"Account\" JOIN \"Card\" ON" +
                         $" \"Account\".\"AccountNumber\" = \"Card\".\"AccountNumber\" WHERE \"CardNumber\"=@cardnumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@cardnumber", CardNumberTo.Replace(" ", ""));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var accountNumber = reader.GetString(0);
                        var balance = reader.GetDecimal(1);
                        var piggyBank = reader.GetDecimal(2);
                        AccountTo = new Account(accountNumber, balance, piggyBank);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }

        private bool IsValidTransaction()
        {
            if (PaymentType != "Main")
            {
                return Amount <= AccountFrom.PiggyBankDecimal && Amount <= AccountFrom.AccountCard.Limit;
            }
            return Amount <= AccountFrom.BalanceDecimal && Amount <= AccountFrom.AccountCard.Limit;
        }

        public override string ToString()
        {
            return $"{nameof(CardNumberTo)}: {CardNumberTo}, {nameof(AccountFrom)}: {AccountFrom}, {nameof(Amount)}: {Amount}";
        }
    }
}
