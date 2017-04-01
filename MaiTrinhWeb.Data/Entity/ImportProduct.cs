using System;
using System.ComponentModel.DataAnnotations;

namespace MaiTrinhWeb.Data
{
    public class ImportProduct : IEntity
    {
        public Guid Id { get; set; }

        [Display(Name = "Ngày nhập")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ImportDate { get; set; }

        [Display(Name = "Sản phẩm")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng điền số lượng lớn hơn 0.")]
        public int Quantity { get; set; }

        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

    }
}
