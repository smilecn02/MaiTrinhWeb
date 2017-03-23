using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiTrinhWeb.Data
{
    public class SellProduct
    {
        public Guid Id { get; set; }

        [Display(Name = "Ngày bán")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExportDate { get; set; }

        [Display(Name = "Sản phẩm")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng điền số lượng lớn hơn 0.")]
        public int Quantity { get; set; }

        [Display(Name = "Giá bán")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "Giá vận chuyển")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal ShipPrice { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Khách hàng")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
