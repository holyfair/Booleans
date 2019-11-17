using System.Windows.Input;
using Booleans.Models;
using Booleans.ViewModels;
using MahApps.Metro.Controls;

namespace Booleans.Views
{
   
    public partial class RecipientView : MetroWindow
    {
        internal RecipientView(TransferDB transferDb, Client client)
        {
            InitializeComponent();
            DataContext = new RecipientViewModel(client.Name, client.Surname, transferDb, this.Close);
        }
    }
}
