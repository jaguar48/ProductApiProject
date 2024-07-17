

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI_BLL.Interface;
using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Dtos.Response;
using ProductAPI_Data.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductApi_PLL.Controllers
{
    [ApiController]
    [Route("api/productapp")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productServices)
        {
            _productService = productServices;

        }

        [Authorize(Roles = "Seller")]
        [HttpPost("create")]
        [SwaggerOperation("New product creation.")]
        [SwaggerResponse(200, "The product has been successfully created.", typeof(CreateProductRequest))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest productRequest)
        {
            try
            {
                var result = await _productService.CreateProductAsync(productRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Seller")]
        [HttpPut("edit/{id}")]
        [SwaggerOperation(Summary = "Update a product.", Description = "Requires seller authorization.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The product was successfully updated.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, CreateProductRequest productDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller/products")]
        [SwaggerOperation(Summary = "Get seller products.", Description = "Requires seller authorization.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The seller products were successfully retrieved.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<ActionResult<List<ProductResponse>>> GetSellerProducts()
        {

            var products = await _productService.GetSellerProductsAsync();
            return Ok(products);
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("delete/{id}")]
        [SwaggerOperation(Summary = "Delete a product.", Description = "Requires seller authorization.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was successfully deleted.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a product by ID.", Description = "Requires seller authorization.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the product.", typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permission to view this product.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
          
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            
        }
    }
}
