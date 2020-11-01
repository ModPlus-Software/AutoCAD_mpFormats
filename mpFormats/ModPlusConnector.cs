namespace mpFormats
{
    using System;
    using System.Collections.Generic;
    using ModPlusAPI.Abstractions;
    using ModPlusAPI.Enums;

    public class ModPlusConnector : IModPlusPlugin
    {
        private static ModPlusConnector _instance;

        public static ModPlusConnector Instance => _instance ?? (_instance = new ModPlusConnector());

        /// <inheritdoc/>
        public SupportedProduct SupportedProduct => SupportedProduct.AutoCAD;

        /// <inheritdoc/>
        public string Name => "mpFormats";

#if A2013
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2013";
#elif A2014
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2014";
#elif A2015
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2015";
#elif A2016
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2016";
#elif A2017
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2017";
#elif A2018
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2018";
#elif A2019
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2019";
#elif A2020
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2020";
#elif A2021
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2021";
#endif

        /// <inheritdoc/>
        public string FullClassName => string.Empty;

        /// <inheritdoc/>
        public string AppFullClassName => string.Empty;

        /// <inheritdoc/>
        public Guid AddInId => Guid.Empty;

        /// <inheritdoc/>
        public string LName => "Форматки";

        /// <inheritdoc/>
        public string Description => "Плагин вставки в чертеж форматок по ГОСТ 2.301-68/ISO 216";

        /// <inheritdoc/>
        public string Author => "Пекшев Александр aka Modis";

        /// <inheritdoc/>
        public string Price => "0";

        /// <inheritdoc/>
        public bool CanAddToRibbon => true;

        /// <inheritdoc/>
        public string FullDescription => "Форматки вставляются в виде блока со специальной меткой в расширенных данных. Метка в расширенных данных требуется для определения форматок другими функциями (например, \"Нумератор листов\"). Поэтому не следует вставлять форматки стандартными средствами вставки блока в AutoCAD. Плагин позволяет добавлять к форматке штампы – для этого должна быть установлена и хотя бы раз запущена функция \"Штампы\"";

        /// <inheritdoc/>
        public string ToolTipHelpImage => string.Empty;

        /// <inheritdoc/>
        public List<string> SubPluginsNames => new List<string>();

        /// <inheritdoc/>
        public List<string> SubPluginsLNames => new List<string>();
        
        /// <inheritdoc/>
        public List<string> SubDescriptions => new List<string>();

        /// <inheritdoc/>
        public List<string> SubFullDescriptions => new List<string>();

        /// <inheritdoc/>
        public List<string> SubHelpImages => new List<string>();

        /// <inheritdoc/>
        public List<string> SubClassNames => new List<string>();
    }
}
