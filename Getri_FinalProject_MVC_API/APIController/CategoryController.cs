using AutoMapper;
using Domain.Models;
using Getri_FinalProject_MVC_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace Getri_FinalProject_MVC_API.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }

        [HttpGet("GetCategoryList")]
        public ActionResult GetCategories()
        {
               return Ok(categoryRepository.GetCategories());
        }

        [HttpGet("GetCategoryById")]
        public ActionResult GetCategory(int id)
        {
            var category = categoryRepository.SearchCategory(id);
            return Ok(category);
        }

        [HttpPost("InsertCategory")]
        public ActionResult CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            categoryRepository.CreateCategory(category);
            return Ok();
        }

        [HttpPut("EditCategory")]
        public ActionResult UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            categoryRepository.UpdateCategory(category);
            return Ok();
        }

        [HttpDelete("RemoveCategory")]
        public ActionResult DeleteCategory(int id)
        {
            categoryRepository.DeleteCategory(id);
            return Ok();
        }
    }
}
