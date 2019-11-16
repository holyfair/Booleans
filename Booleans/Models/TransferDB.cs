using System;

namespace Booleans.Models
{
    internal class TransferDB : Transfer
    {
        public static void SaveTransferToDB()
        {
            //string sql = "UPDATE \"Account\" SET " +
            //             "(\"AccountNumberTo\", \"AccountNumberFrom\", \"Money\", \"Date\") " +
            //             "VALUES(@accountNumberTo, @accountNumberFrom, @money, @date)";

            //using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            //{
            //    decimal rest = Amount % 10;
            //    // create parameters
            //    command.Parameters.AddWithValue("@accountNumberTo", AccountTo.AccountCard.AccountNumber);
            //    command.Parameters.AddWithValue("@accountNumberFrom", AccountFrom.AccountCard.AccountNumber);
            //    command.Parameters.AddWithValue("@money", NpgsqlDbType.Money);
            //    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date);

            //    command.ExecuteNonQuery();
            //}
        }
    }
}
