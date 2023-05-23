using Aw3.Base.Model;
using Aw3.Schema.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aw3.Schema.Product
{
    public class ProductResponse : BaseResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
