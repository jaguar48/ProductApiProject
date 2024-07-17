using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductAPI_BLL.Interface;
using ProductAPI_Contracts;
using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Dtos.Response;
using ProductAPI_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_BLL.Implementation
{


    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;




        public ProductService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;

            _unitOfWork = unitOfWork;


            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            _productRepo = _unitOfWork.GetRepository<Product>();

        }

        public async Task<string> CreateProductAsync(CreateProductRequest productDto)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("User not found");
                }

                Seller seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == userId);

                if (seller == null)
                {
                    throw new Exception("Seller not found");
                }

                var product = new Product
                {
                    Title = productDto.Title,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    CreatedAt = DateTime.UtcNow.ToString("o"),
                    UpdatedAt = DateTime.UtcNow.ToString("o"),
                    SellerId = seller.Id
                };

                await _productRepo.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                var result = new { success = true, message = "Product created successfully" };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }


        public async Task<string> UpdateProductAsync(int productId, CreateProductRequest productDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User not found");
            }

            var existingProduct = await _productRepo.GetSingleByAsync(x => x.Id == productId, include: x => x.Include(p => p.Seller));

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            if (existingProduct.Seller.UserId != userId)
            {
                throw new Exception("You do not have permission to update this product");
            }

            existingProduct.Title = productDto.Title;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.UpdatedAt = DateTime.UtcNow.ToString("o");

            _productRepo.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            var result = new { success = true, message = "Product updated successfully" };
            return JsonConvert.SerializeObject(result);
        }

        public async Task<List<ProductResponse>> GetSellerProductsAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            Seller seller = await _sellerRepo.GetSingleByAsync(x => x.UserId == userId);

            if (seller == null)
            {
                throw new Exception("Seller not found");
            }

            IEnumerable<Product> sellerProducts = await _productRepo.GetByAsync(p => p.SellerId == seller.Id);

            List<ProductResponse> productResponses = sellerProducts.Select(p => new ProductResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                SellerId = p.SellerId,

            }).ToList();

            return productResponses;
        }

        public async Task<string> DeleteProductAsync(int productId)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User not found");
            }

            var existingProduct = await _productRepo.GetSingleByAsync(x => x.Id == productId, include: x => x.Include(s => s.Seller));

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }


            var buyer = await _sellerRepo.GetSingleByAsync(b => b.UserId == userId);



            if (existingProduct.Seller.UserId != userId)
            {
                throw new Exception("You do not have permission to delete this product");
            }

            await _productRepo.DeleteAsync(existingProduct);


            var result = new { success = true, message = "Product deleted successfully" };
            return JsonConvert.SerializeObject(result);


        }
        public async Task<ProductResponse> GetProductByIdAsync(int productId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var product = await _productRepo.GetSingleByAsync(p => p.Id == productId, include: p => p.Include(p => p.Seller));

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            
            var seller = await _sellerRepo.GetSingleByAsync(s => s.Id == product.SellerId);

            if (seller == null || seller.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to view this product");
            }

            
            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                SellerId = product.SellerId,
               
            };

            return productResponse;
        }
    }

}

