
using Booleans.Tools.Navigation;
using Booleans.Views;
using System;

namespace HappyTravel.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }


        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.SignIn:
                    ViewsDictionary.Add(viewType, new SignInView());
                    break;
                case ViewType.Welcome:
                    ViewsDictionary.Add(viewType, new WelcomeView());
                    break;
                case ViewType.Transfer:
                    ViewsDictionary.Add(viewType, new TransferView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
