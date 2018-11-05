﻿using System;
using System.Collections.Generic;
using ModPlusAPI.Interfaces;

namespace mpFormats
{
    public class Interface : IModPlusFunctionInterface
    {
        private static Interface _instance;

        public static Interface Instance => _instance ?? (_instance = new Interface());

        public SupportedProduct SupportedProduct => SupportedProduct.AutoCAD;
        public string Name => "mpFormats";
        public string AvailProductExternalVersion => "2017";
        public string FullClassName => string.Empty;
        public string AppFullClassName => string.Empty;
        public Guid AddInId => Guid.Empty;
        public string LName => "Форматки";
        public string Description => "Функция вставки в чертеж форматок по ГОСТ 2.301-68";
        public string Author => "Пекшев Александр aka Modis";
        public string Price => "0";
        public bool CanAddToRibbon => true;
        public string FullDescription => "Форматки вставляются в виде блока со специальной меткой в расширенных данных. Метка в расширенных данных требуется для определения форматок другими функциями (например, \"Нумератор листов\"). Поэтому не следует вставлять форматки стандартными средствами вставки блока в AutoCAD. Функция позволяет добавлять к форматке штампы – для этого должна быть установлена и хотя бы раз запущена функция \"Штампы\"";
        public string ToolTipHelpImage => string.Empty;
        public List<string> SubFunctionsNames => new List<string>();
        public List<string> SubFunctionsLames => new List<string>();
        public List<string> SubDescriptions => new List<string>();
        public List<string> SubFullDescriptions => new List<string>();
        public List<string> SubHelpImages => new List<string>();
        public List<string> SubClassNames => new List<string>();
    }
}
