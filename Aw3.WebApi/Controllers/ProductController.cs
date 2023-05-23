namespace Aw3.WebApi.Controllers
{
    using AutoMapper;
    using Aw3.Data.Model;
    using Aw3.DataAccess.UnitOfWork;
    using Aw3.Schema.Category;
    using Aw3.Schema.Product;
    using Aw3.WebApi.Mapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("simapi/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public List<ProductResponse> GetAll()
        {
            var productList = unitOfWork.GetRepository<Product>().GetAll().Include(p => p.Category).ToList();
            var productResponseList = mapper.Map<List<ProductResponse>>(productList);
            return productResponseList;
        }

        [HttpGet("{id}")]
        public ProductResponse GetById(int id)
        {
            var product = unitOfWork.GetRepository<Product>().GetAll(x => x.Id.Equals(id)).Include(p => p.Category)?.FirstOrDefault();
            var mapped = mapper.Map<Product, ProductResponse>(product);
            return mapped;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductRequest request)
        {
            var product = mapper.Map<ProductRequest, Product>(request);
            unitOfWork.GetRepository<Product>().Add(product);
            if (unitOfWork.SaveChanges() > 0)
            {
                return Ok("Product Added.");
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductRequest request)
        {
            var product = unitOfWork.GetRepository<Product>().Get(x => x.Id.Equals(id));
            if (product != null)
            {
                mapper.Map(request, product);
                unitOfWork.GetRepository<Product>().Update(product);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return Ok("Product Updated.");
                }

                return StatusCode(500);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = unitOfWork.GetRepository<Product>().Get(x => x.Id.Equals(id));
            if (product != null)
            {
                unitOfWork.GetRepository<Product>().Delete(product);
                if (unitOfWork.SaveChanges() > 0)
                {
                    return Ok("Product Deleted.");
                }

                return StatusCode(500);
            }
            return BadRequest();
        }

    }

}
