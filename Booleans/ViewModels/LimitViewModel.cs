using Booleans.Models;
using Booleans.Tools;
using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.ViewModels
{
    internal class LimitViewModel : BaseViewModel
    {
        private decimal _limit;
        private RelayCommand<object> _backCommand;
        private RelayCommand<object> _changeLimitCommand;
        private Action close;

        public decimal Limit
        {
            get => _limit;
            set
            {
                _limit = value;
                OnPropertyChanged();
            }
        }
        public LimitViewModel(Action close)
        {
            this.close = close;
        }

        public RelayCommand<Object> BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand<object>(o => close()));
            }
        }

        public RelayCommand<Object> ChangeLimitCommand
        {
            get
            {
                return _changeLimitCommand ?? (_changeLimitCommand = new RelayCommand<object>(o => ChangeLimit()));
            }
        }

        private void ChangeLimit()
        {
            string sql = "UPDATE \"Account\" SET " +
                         "\"Limit\" = @limit Where \"AccountNumber\" = @accountNumber";
            using (NpgsqlCommand command = new NpgsqlCommand(sql, ConnectionManager.GetInstance().Connection))
            {
                command.Parameters.AddWithValue("@limit", Limit);
                command.Parameters.AddWithValue("@accountNumber", StationManager.DataStorage.CurrentAccount.AccountCard.AccountNumber);

                command.ExecuteNonQuery();
            }
            close.Invoke();
        }
    }
}