using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Data
{
    public class Color : IEntity
    {
        public Guid Id { get; set; }

        [Display(Name="Tên màu")]
        public string Name { get; set; }
    }
}
