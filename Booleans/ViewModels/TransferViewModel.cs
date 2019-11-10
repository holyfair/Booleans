using System;
using Booleans.Tools;
using Booleans.Tools.Managers;

namespace Booleans.ViewModels
{
    class TransferViewModel: BaseViewModel
    {
        private RelayCommand<object> _closeCommand;

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => StationManager.CloseApp()));
            }
        }
    }
}
