using Booleans.DataStorage;
using System;
using System.Windows;

namespace Booleans.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;
        public static bool IsControlEnabled { get; set; }
        public static bool IsLoaderActive { get; set; }

        internal static IDataStorage DataStorage => _dataStorage;

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            IsControlEnabled = true;
            IsLoaderActive = false;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}
