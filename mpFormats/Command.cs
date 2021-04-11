namespace mpFormats
{
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.Runtime;
    using View;
    using ViewModels;

    /// <summary>
    /// Command starter
    /// </summary>
    public class Command
    {
        private MainWindow _mainWindow;

        /// <summary>
        /// Start "Formats" command
        /// </summary>
        [CommandMethod("ModPlus", "mpFormats", CommandFlags.Modal)]
        public void StartMpFormats()
        {
#if !DEBUG
            ModPlusAPI.Statistic.SendCommandStarting(new ModPlusConnector());
#endif

            if (_mainWindow == null)
            {
                _mainWindow = new MainWindow();
                var context = new MainContext(_mainWindow);
                _mainWindow.DataContext = context;
                _mainWindow.Closed += (sender, args) => _mainWindow = null;
            }

            if (_mainWindow.IsLoaded)
                _mainWindow.Activate();
            else
                Application.ShowModelessWindow(Application.MainWindow.Handle, _mainWindow, false);
        }
    }
}
