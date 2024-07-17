
using Microsoft.AspNetCore.Mvc;
using ProductAPI_BLL.Interface;
using ProductAPI_Data.Dtos.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace EventBookingApp_PLL.Controllers
{
    [ApiController]
    [Route("api/productapi")]
    public class CustomerController : ControllerBase
    {
        private readonly ISellerService _sellerServices;


        public CustomerController(ISellerService sellerServices)
        {
            _sellerServices = sellerServices;

        }
        [HttpPost("seller-register")]

        [SwaggerOperation("New seller registration.")]
        [SwaggerResponse(200, "The seller has been successfully registered.", typeof(SellerRegistrationRequest ))]
        public async Task<IActionResult> RegisterCustomer([FromBody] SellerRegistrationRequest sellerRegistrationRequest)
        {

            var result = await _sellerServices.RegisterCustomer(sellerRegistrationRequest);
            return Ok(result);
        }
    }
}


