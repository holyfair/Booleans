using System.Windows.Controls;
using Booleans.Tools.Navigation;
using Booleans.ViewModels;

namespace Booleans.Views
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : UserControl, INavigatable
    {
        public WelcomeView()
        {
            InitializeComponent();
            DataContext = new WelcomeViewModel();
        }
    }
}
