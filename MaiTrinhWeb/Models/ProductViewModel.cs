using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Mã")]
        public string Code { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Images { get; set; }

        [Display(Name = "Kích cỡ")]
        public string Size { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Màu sắc")]
        public string ColorName { get; set; }

        [Display(Name = "Thẻ đánh dấu")]
        public string Tags { get; set; } //Indicate same kind product or Category
    }
}