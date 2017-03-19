using System;

namespace MaiTrinhWeb.Data
{
    public class Color : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
