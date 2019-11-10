namespace Booleans.Tools.Navigation
{
    internal enum ViewType
    {
        SignIn,
        Welcome,
        Transfer
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
