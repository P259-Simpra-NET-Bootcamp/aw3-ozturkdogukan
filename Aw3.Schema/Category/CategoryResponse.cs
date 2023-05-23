using Aw3.Base.Model;
using Aw3.Data.Model;
using Aw3.Schema.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aw3.Schema.Category
{
    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}
