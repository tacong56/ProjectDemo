using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Data.Enum;

namespace WebApi.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }
        public List<ProductInCategory> productInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
