using Booleans.Tools;
using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booleans.ViewModels
{
    internal class LimitViewModel : BaseViewModel
    {
        private RelayCommand<object> _backCommand;
        private Action close;

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
    }
}