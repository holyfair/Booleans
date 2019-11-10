using Booleans.Tools.Managers;
using Booleans.Tools.Navigation;
using Booleans.ViewModels;
using HappyTravel.Tools.Navigation;
using System.Windows;
using System.Windows.Controls;
using Booleans.DataStorage;
using MahApps.Metro.Controls;

namespace Booleans
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }
    }
}