using System;
using Booleans.Tools.Managers;
using Npgsql;
using NpgsqlTypes;

namespace Booleans.Models
{
    internal class TransferDB : Transfer
    {
        public TransferDB(string cardNumberTo, Account accountFrom, decimal amount, string paymentType)
            : base(cardNumberTo, accountFrom, amount, paymentType)
        {
        }

        public void SaveTransferToDB()
        {
            string sql = "INSERT INTO \"Transfer\" " +
                         "(\"AccountNumberTo\", \"AccountNumberFrom\", \"Money\", \"Date\", \"PaymentType\") " +
                         "VALUES(@accountNumberTo, @accountNumberFrom, @money, @date, @paymentType)";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@accountNumberTo", AccountTo.AccountCard.AccountNumber);
                command.Parameters.AddWithValue("@accountNumberFrom", AccountFrom.AccountCard.AccountNumber);
                command.Parameters.AddWithValue("@money", Amount);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@paymentType", PaymentType);

                command.ExecuteNonQuery();
            }
        }
    }
}
