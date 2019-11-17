using System;
using System.Windows;
using Booleans.Tools.Managers;
using Npgsql;
using NpgsqlTypes;

namespace Booleans.Models
{
    internal class Transfer : ITransfer
    {
        public string CardNumberTo { get; set; }
        public Account AccountFrom { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }


        private Account AccountTo { get; set; }

        public Transfer(string cardNumberTo, Account accountFrom, decimal amount, string paymentType)
        {
            CardNumberTo = cardNumberTo;
            AccountFrom = accountFrom;
            Amount = amount;
            PaymentType = paymentType;
        }

        public Transfer()
        {
        }

        public virtual void DoTransfer()
        {
            if (IsValidTransaction())
            {
                if (IsAccountToValid())
                {
                    WithdrawMoney(PaymentType);
                    DepositMoney();
                    MessageBox.Show("Successful");
                }
                else
                {
                    MessageBox.Show("Card number does not exist!");
                }
            }
            else
            {
                MessageBox.Show("Not enough money or amount is bigger then limit");
            }
        }

        private void DepositMoney()
        {
            string sql = "UPDATE \"Account\" SET " +
                         "\"Balance\" = @balance, \"PiggyBank\" = @piggyBank Where \"AccountNumber\" = @accountNumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                var rest = Amount % 10;
                var newBalance = AccountTo.BalanceDecimal + (Amount - rest);

                command.Parameters.AddWithValue("@balance", newBalance);
                command.Parameters.AddWithValue("@piggyBank", rest);
                command.Parameters.AddWithValue("@accountNumber", AccountTo.AccountCard.AccountNumber);

                command.ExecuteNonQuery();
            }
        }

        private void WithdrawMoney(string paymentType)
        {
            string sql = "";
            if (paymentType == "Main")
            {
                sql = "UPDATE \"Account\" SET " +
                      "\"Balance\" = @balance Where \"AccountNumber\" = @accountNumber";
            }
            else
            {
                sql = "UPDATE \"Account\" SET " +
                      "\"PiggyBank\" = @balance Where \"AccountNumber\" = @accountNumber";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                var newBalance = StationManager.DataStorage.CurrentAccount.BalanceDecimal - Amount;
                command.Parameters.AddWithValue("@balance", newBalance);
                command.Parameters.AddWithValue("@accountNumber", AccountTo.AccountCard.AccountNumber);

                command.ExecuteNonQuery();
            }
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
