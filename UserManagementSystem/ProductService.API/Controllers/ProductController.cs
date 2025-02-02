using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Repositories;
using ProductService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepository, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ILogger<ProductController> _logger = logger;

        [SwaggerOperation(Summary = "Get all products", Description = "Retrieves all products from the database")]
        [SwaggerResponse(200, "Products found", typeof(Product))]
        [SwaggerResponse(204, "Product not found")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetProductsAsync();
                if (products != null)
                {
                    return Ok(products);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [SwaggerOperation(Summary = "Get a product by ID", Description = "Retrieves a product from the database")]
        [SwaggerResponse(200, "Product found", typeof(Product))]
        [SwaggerResponse(404, "Product not found")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpGet("id")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product != null)
                {
                    return Ok(product);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [SwaggerOperation(Summary = "Create a new product", Description = "Adds a new product")]
        [SwaggerResponse(201, "Product created successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductRequestDTO productRequestDTO)
        {
            try
            {
                var product = await _productRepository.AddProductAsync(productRequestDTO);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [SwaggerOperation(Summary = "Update an existing product", Description = "Update an existing product")]
        [SwaggerResponse(201, "Product update successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductRequestDTO productRequestDTO)
        {
            try
            {
                var product = await _productRepository.UpdateProductAsync(id, productRequestDTO);
                if (product != null)
                {
                    return Ok(product);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [SwaggerOperation(Summary = "Delete product", Description = "Delete product")]
        [SwaggerResponse(201, "Product deleted successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        { 
            try
            {
                var result = await _productRepository.DeleteProductAsync(id);
                if (result)
                {
                    return Ok("Delete Successful");
                }
                return BadRequest("Record not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [SwaggerOperation(Summary = "Search Product", Description = "Search Product By Product Name")]
        [SwaggerResponse(200, "Products found", typeof(Product))]
        [SwaggerResponse(204, "Products not found")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByNameAsync(string name)
        {
            try
            {
                var products = await _productRepository.SearchProductByNameAsync(name);
                if (products != null)
                {
                    return Ok(products);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
