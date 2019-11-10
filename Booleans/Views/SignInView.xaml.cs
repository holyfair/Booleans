using Booleans.Tools.Navigation;
using Booleans.ViewModels;
using System.Windows.Controls;

namespace Booleans.Views
{
    public partial class SignInView : UserControl, INavigatable
    {
        internal SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel(this.Pin, this.CardNumber);
        }
    }
}