using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Objects;

namespace WebApi.Backend.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public DateTime DateCreated { set; get; }
    }

    #region:Request
    public class CreateProductRequest
    {
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }

        //[Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }

        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public bool? IsFeatured { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }

    public class GetProductRequest : BaseRequest
    {
        public string Keyword { get; set; }
        public string LanguageId { get; set; }
        public List<int> CategoryIds { get; set; }
    }
    #endregion

    #region:Response

    #endregion
}
