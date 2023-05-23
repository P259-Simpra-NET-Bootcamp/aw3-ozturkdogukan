namespace Aw3.WebApi.Controllers
{
    using AutoMapper;
    using Aw3.Data.Model;
    using Aw3.DataAccess.UnitOfWork;
    using Aw3.Schema.Category;
    using Aw3.WebApi.Mapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    [Route("simapi/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public List<CategoryResponse> GetAll()
        {
            var categoryList = unitOfWork.GetRepository<Category>().GetAll().Include(p => p.Products).ToList();
            var mapped = mapper.Map<List<Category>, List<CategoryResponse>>(categoryList);
            return mapped;
        }

        [HttpGet("{id}")]
        public CategoryResponse GetById(int id)
        {
            var category = unitOfWork.GetRepository<Category>().GetAll(x => x.Id.Equals(id)).Include(p => p.Products)?.FirstOrDefault();
            var mapped = mapper.Map<Category, CategoryResponse>(category);
            return mapped;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryRequest request)
        {
            var category = mapper.Map<CategoryRequest, Category>(request);
            unitOfWork.GetRepository<Category>().Add(category);
            if (unitOfWork.SaveChanges() > 0)
            {
                return Ok("Product Added.");
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryRequest request)
        {
            var category = unitOfWork.GetRepository<Category>().Get(x => x.Id.Equals(id));
            if (category != null)
            {
                var categoryObject = mapper.Map<CategoryRequest, Category>(request, category);
                unitOfWork.GetRepository<Category>().Update(categoryObject);
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
            var category = unitOfWork.GetRepository<Category>().Get(x => x.Id.Equals(id));
            if (category != null)
            {
                unitOfWork.GetRepository<Category>().Delete(category);
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
