using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiTrinhWeb.Data
{
    public class ImportProduct : IEntity
    {
        public Guid Id { get; set; }

        [Display(Name = "Ngày nhập")]
        public DateTime ImportDate { get; set; }

        [Display(Name = "Sản phẩm")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

    }
}
