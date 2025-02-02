using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Repositories;
using ProductService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [SwaggerOperation(Summary = "Get all products", Description = "Retrieves all products from the database")]
        [SwaggerResponse(200, "Products found", typeof(Product))]
        [SwaggerResponse(204, "Product not found")]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            if (products != null)
            {
                return Ok(products);
            }
            return NoContent();

        }

        [SwaggerOperation(Summary = "Get a product by ID", Description = "Retrieves a product from the database")]
        [SwaggerResponse(200, "Product found", typeof(Product))]
        [SwaggerResponse(404, "Product not found")]
        [HttpGet("id")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();

        }

        [SwaggerOperation(Summary = "Create a new product", Description = "Adds a new product")]
        [SwaggerResponse(201, "Product created successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductRequestDTO productRequestDTO)
        {
            var product = await _productRepository.AddProductAsync(productRequestDTO);
            return Ok(product);
        }

        [SwaggerOperation(Summary = "Update an existing product", Description = "Update an existing product")]
        [SwaggerResponse(201, "Product update successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] UpdateProductRequestDTO productRequestDTO)
        {
            var product = await _productRepository.UpdateProductAsync(id, productRequestDTO);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();

        }

        [SwaggerOperation(Summary = "Delete product", Description = "Delete product")]
        [SwaggerResponse(201, "Product deleted successfully", typeof(Product))]
        [SwaggerResponse(400, "Invalid request data")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (result)
            {
                return Ok("Delete Successful");
            }
            return BadRequest("Record not found");

        }
    }
}
