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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductController(IProductRepository _productRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            mapper = _mapper;
        }

        [HttpPost]
        [Route("InsertProduct")]
        public IActionResult InsertProduct(ProductDTO productDTO)
        {
            var productId = productRepository.CreateProduct(mapper.Map<Product>(productDTO));
            return Ok(productId);                        
        }

        [HttpGet]
        [Route("GetProductList")]
        public IActionResult GetProducts()
        {
            return Ok(productRepository.GetProducts());
        }
    }
}
