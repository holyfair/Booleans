using Booleans.Models;
using Booleans.Tools;
using Booleans.Tools.Managers;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.ViewModels
{
    internal class TransferHistoryViewModel : BaseViewModel
    {
        private RelayCommand<object> _closeCommand;
        public List<TransferDBO> CurrentHistory { get; set; }
        private Action close;

        public TransferHistoryViewModel(Action close)
        {
            this.close = close;
            CurrentHistory = GenerateHistory();
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => close()));
            }
        }

        private List<TransferDBO> GenerateHistory()
        {
            var acc = StationManager.DataStorage.CurrentAccount.AccountCard.AccountNumber;
            string sql = $"SELECT \"CardNumber\", \"Money\", \"Name\", \"Surname\" " +
                         $"FROM (\"Client\" INNER JOIN \"Account\" ON \"Client\".\"ClientId\" = \"Account\".\"ClientId\") INNER JOIN \"Card\" ON \"Account\".\"AccountNumber\" = \"Card\".\"AccountNumber\" " +
                         $"INNER JOIN \"Transfer\" " +
                         $"ON \"Account\".\"AccountNumber\" = \"Transfer\".\"AccountNumberTo\" " +
                         $"WHERE \"AccountNumberFrom\" = \'{acc}\';";
            using (NpgsqlConnection conn = ConnectionManager.GetInstance().Connection)
            {
                return conn.Query<TransferDBO>(sql).ToList();
            }
        }
    }
}