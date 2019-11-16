

using System.Windows;
using Booleans.Tools.Managers;
using Npgsql;

namespace Booleans.Models
{
    internal class Transfer : ITransfer
    {
        public string CardNumberTo { get; private set; }
        public Account AccountFrom { get; set; }
        public decimal Amount { get; set; }

        public Transfer(string cardNumberTo, Account accountFrom, decimal amount)
        {
            CardNumberTo = cardNumberTo;
            AccountFrom = accountFrom;
            Amount = amount;
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

        private bool IsAccountToValid()
        {
            string sql = $"SELECT * FROM \"Card\" WHERE \"CardNumber\"=@cardnumber";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@cardnumber", CardNumberTo.Replace(" ", ""));
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }

            }
        }

        private bool IsValidTransaction()
        {
            return Amount <= AccountFrom.BalanceDecimal && Amount <= AccountFrom.AccountCard.Limit;
        }

        public override string ToString()
        {
            return $"{nameof(CardNumberTo)}: {CardNumberTo}, {nameof(AccountFrom)}: {AccountFrom}, {nameof(Amount)}: {Amount}";
        }
    }
}
