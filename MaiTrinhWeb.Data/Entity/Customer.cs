using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Data
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
    }
}
