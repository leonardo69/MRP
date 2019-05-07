using System.Collections.Generic;
// ReSharper disable CommentTypo

namespace MRP.Entities
{
    public class Component
    {
        public int Id { get; set; }
        /// <summary>
        /// Название компонента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// Количество на складе
        /// </summary>
        public int? CountInStore { get; set; }
        /// <summary>
        /// Объём партии
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// Сборка
        /// </summary>
        public int? AssemblyId { get; set; }
        public Component Parent { get; set; }
        public ICollection<Component> Children { get; set; }
    }
}
