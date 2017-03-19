using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Data
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }

        [Display(Name = "Mã")]
        public string Code { get; set; }

        [Display(Name = "Tên sản phẩm")]
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
        public Guid? ColorId { get; set; }
        public virtual Color Color { get; set; }

        [Display(Name = "Thẻ đánh dấu")]
        public string Tags { get; set; } //Indicate same kind product or Category

        [Display(Name = "Nhà cung cấp")]
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
