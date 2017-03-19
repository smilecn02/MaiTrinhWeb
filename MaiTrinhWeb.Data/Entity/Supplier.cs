using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Data
{
    public class Supplier : IEntity
    {
        public Guid Id { get; set; }
        [Display(Name = "Tên")]
        public string  Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string  Address { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
