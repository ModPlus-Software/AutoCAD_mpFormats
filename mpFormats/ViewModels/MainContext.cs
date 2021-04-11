namespace mpFormats.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using ModPlusAPI;
    using ModPlusAPI.Mvvm;
    using View;

    /// <summary>
    /// Main context
    /// </summary>
    public class MainContext : VmBase
    {
        private readonly MainWindow _mainWindow;
        private string _selectedGostFormatName;
        private string _selectedIsoFormatName;
        private int _selectedMultiplicity;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainContext"/> class.
        /// </summary>
        /// <param name="mainWindow"><see cref="MainWindow"/> instance</param>
        public MainContext(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            GostFormatNames = new[]
            {
                "A0", "A1", "A2", "A3", "A4", "A5"
            };
            IsoFormatNames = new[]
            {
                "A0", "A1", "A2", "A3", "A4", "A5", "A6",
                "B0", "B1", "B2", "B3", "B4", "B5", "B6",
                "C0", "C1", "C2", "C3", "C4", "C5"
            };

            MultiplicityValues = new[] { 1 };

            LoadFromSettings();
        }

        /// <summary>
        /// Имена форматок по ГОСТ
        /// </summary>
        public string[] GostFormatNames { get; }

        /// <summary>
        /// Имена форматок по ISO
        /// </summary>
        public string[] IsoFormatNames { get; }

        /// <summary>
        /// Значения свойства "Кратность"
        /// </summary>
        public int[] MultiplicityValues { get; private set; }

        /// <summary>
        /// Выбранная кратность
        /// </summary>
        public int SelectedMultiplicity
        {
            get => _selectedMultiplicity;
            set
            {
                if (_selectedMultiplicity == value)
                    return;
                _selectedMultiplicity = value;
                OnPropertyChanged();
                UserConfigFile.SetValue(ModPlusConnector.Instance.Name, nameof(SelectedMultiplicity), value.ToString(), true);
            }
        }

        /// <summary>
        /// Выбранное имя форматки по ГОСТ
        /// </summary>
        public string SelectedGostFormatName
        {
            get => _selectedGostFormatName;
            set
            {
                if (_selectedGostFormatName == value)
                    return;
                _selectedGostFormatName = value;
                OnPropertyChanged();
                UserConfigFile.SetValue(ModPlusConnector.Instance.Name, nameof(SelectedGostFormatName), value, true);
                FillMultiplicity(value);
                
                SelectedMultiplicity = int.TryParse(UserConfigFile.GetValue(ModPlusConnector.Instance.Name, nameof(SelectedMultiplicity)), out var i)
                    ? MultiplicityValues.Contains(i) ? SelectedMultiplicity = i : SelectedMultiplicity = MultiplicityValues[0]
                    : MultiplicityValues[0];
            }
        }

        /// <summary>
        /// Выбранное имя форматки по ISO
        /// </summary>
        public string SelectedIsoFormatName
        {
            get => _selectedIsoFormatName;
            set
            {
                if (_selectedIsoFormatName == value)
                    return;
                _selectedIsoFormatName = value;
                OnPropertyChanged();
                UserConfigFile.SetValue(ModPlusConnector.Instance.Name, nameof(SelectedIsoFormatName), value, true);
            }
        }

        private void LoadFromSettings()
        {
            var e = ModPlusConnector.Instance.Name;
            var savedGostFormatName = UserConfigFile.GetValue(e, nameof(SelectedGostFormatName));
            SelectedGostFormatName = GostFormatNames.Contains(savedGostFormatName) ? savedGostFormatName : GostFormatNames[3];

            var savedIsoFormatName = UserConfigFile.GetValue(e, nameof(SelectedIsoFormatName));
            SelectedIsoFormatName = IsoFormatNames.Contains(savedIsoFormatName) ? savedIsoFormatName : IsoFormatNames[3];
        }

        private void FillMultiplicity(string gostFormatName)
        {
            switch (gostFormatName)
            {
                case "A0":
                    MultiplicityValues = new[] { 1, 2, 3 };
                    break;
                case "A1":
                    MultiplicityValues = new[] { 1, 3, 4 };
                    break;
                case "A2":
                    MultiplicityValues = new[] { 1, 3, 4, 5 };
                    break;
                case "A3":
                    MultiplicityValues = new[] { 1, 3, 4, 5, 6, 7 };
                    break;
                case "A4":
                    MultiplicityValues = new[] { 1, 3, 4, 5, 6, 7, 8, 9 };
                    break;
                case "A5":
                    MultiplicityValues = new[] { 1 };
                    break;
            }

            OnPropertyChanged(nameof(MultiplicityValues));
        }
    }
}
