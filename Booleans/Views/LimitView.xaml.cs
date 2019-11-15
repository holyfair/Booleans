using Booleans.Tools.Navigation;
using Booleans.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

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
    }
}