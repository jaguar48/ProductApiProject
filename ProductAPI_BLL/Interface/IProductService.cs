using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_BLL.Interface
{
    public interface IProductService
    {
        Task<string> CreateProductAsync(CreateProductRequest productDto);

        Task<string> UpdateProductAsync(int productId, CreateProductRequest productDto);
        Task<List<ProductResponse>> GetSellerProductsAsync();
        Task<string> DeleteProductAsync(int productId);
        Task<ProductResponse> GetProductByIdAsync(int productId);
    }
}
