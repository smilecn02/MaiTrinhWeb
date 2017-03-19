using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Models
{
    public class InventoryViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Tổng số lượng")]
        public int SumQuantity { get; set; }
    }
}