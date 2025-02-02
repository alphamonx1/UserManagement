using Microsoft.EntityFrameworkCore;
using ProductService.Application.DTOs;
using ProductService.Application.Repositories;
using ProductService.Infrastructure.Database;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponseDTO> AddProductAsync(CreateProductRequestDTO productRequestDTO)
        {
            var product = new Product
            {
                ProductName = productRequestDTO.ProductName,
                Description = productRequestDTO.Description,
                ImageUrl = productRequestDTO.ImageUrl,
                Price = productRequestDTO.Price,
                Quantity = productRequestDTO.Quantity
            };

            var result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return new ProductResponseDTO
            {
                ProductId = result.Entity.ProductId,
                ProductName = result.Entity.ProductName,
                Description = result.Entity.Description,
                ImageUrl = result.Entity.ImageUrl,
                Price = result.Entity.Price,
                Quantity = result.Entity.Quantity
            };
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var result = false;
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product != null)
            {
                _dbContext.Products.Remove(product);
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return result;
        }

        public Task<ProductResponseDTO?> GetProductByIdAsync(int productId)
        {
            var product = _dbContext.Products.Where(p => p.ProductId == productId).Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            }).FirstOrDefaultAsync();

            return product;
        }

        public Task<List<ProductResponseDTO>> GetProductsAsync()
        {
            var products = _dbContext.Products.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToListAsync();

            return products;
        }

        public async Task<ProductResponseDTO> UpdateProductAsync(int productId, UpdateProductRequestDTO productRequestDTO)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.ProductName = productRequestDTO.ProductName;
                product.Description = productRequestDTO.Description;
                product.ImageUrl = productRequestDTO.ImageUrl;
                product.Price = productRequestDTO.Price;
                product.Quantity = productRequestDTO.Quantity;
                await _dbContext.SaveChangesAsync();
                return new ProductResponseDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = product.Quantity
                };
            }
            return null;
        }

        public async Task<List<ProductResponseDTO>> SearchProductByNameAsync(string productName)
        {
            var products = _dbContext.Products.Where(p => p.ProductName.Contains(productName)).Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToListAsync();
            return await products;
        }
    }
}
