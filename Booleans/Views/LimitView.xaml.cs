using Booleans.Tools.Navigation;
using Booleans.ViewModels;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Booleans.Views
{
    /// <summary>
    /// Interaction logic for LimitView.xaml
    /// </summary>
    public partial class LimitView : MetroWindow
    {
        public LimitView()
        {
            InitializeComponent();
            DataContext = new LimitViewModel(this.Close);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}