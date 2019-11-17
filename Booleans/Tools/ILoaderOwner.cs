using System.ComponentModel;
using System.Windows;

namespace Booleans.Tools
{
    internal interface ILoaderOwner
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}
