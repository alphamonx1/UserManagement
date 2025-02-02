using ProductService.Application.DTOs;

namespace ProductService.Application.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductResponseDTO>> GetProductsAsync();
        Task<ProductResponseDTO?> GetProductByIdAsync(int productId);

        Task<List<ProductResponseDTO>> SearchProductByNameAsync(string productName);
        Task<ProductResponseDTO> AddProductAsync(CreateProductRequestDTO productRequestDTO);
        Task<ProductResponseDTO> UpdateProductAsync(int productId, UpdateProductRequestDTO productRequestDTO);
        Task<bool> DeleteProductAsync(int productId);
    }
}
