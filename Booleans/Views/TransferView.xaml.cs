using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Booleans.Tools.Navigation;
using Booleans.ViewModels;

namespace Booleans.Views
{
    /// <summary>
    /// Interaction logic for TransferView.xaml
    /// </summary>
    public partial class TransferView : UserControl, INavigatable
    {
        public TransferView()
        {
            InitializeComponent();
            DataContext = new TransferViewModel();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}