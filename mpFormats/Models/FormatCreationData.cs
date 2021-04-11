namespace mpFormats.Models
{
    /// <summary>
    /// Данные для создания форматки
    /// </summary>
    public class FormatCreationData
    {
        /// <summary>
        /// Имя форматки (А3, А4 и т.п.)
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Ориентация листа
        /// </summary>
        /// TODO To Enum
        public string Orientation { get; set; }
        
        /// <summary>
        /// Кратность
        /// </summary>
        public int Multiplicity { get; set; }
        
        /// <summary>
        /// Сторона кратности
        /// </summary>
        /// TODO To Enum
        public string MultiplicitySide { get; set; }
        
        /// <summary>
        /// Использовать ли размеры форматок по ГОСТ (если false, то размеры берутся путем деления размеров форматки А0)
        /// </summary>
        public bool IsAccordingToGost { get; set; }
        
        /// <summary>
        /// Добавить номер страницы
        /// </summary>
        public bool AddPageNumber { get; set; }
        
        /// <summary>
        /// Нижняя рамка (5 мм / 10 мм)
        /// </summary>
        public string BottomFrame { get; set; }
    }
}
