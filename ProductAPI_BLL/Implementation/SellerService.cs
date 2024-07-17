

using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProductAPI_BLL.Interface;
using ProductAPI_Contracts;
using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Entities;

namespace ProductAPI_BLL.Implementation
{

    public sealed class SellerService : ISellerService
    {
        private readonly IRepository<Seller> _sellerRepo;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserService _userServices;
        private readonly UserManager<User> _userManager;
        /* private readonly IAuthService _authService;*/

       

        public SellerService(IUnitOfWork unitOfWork, UserManager<User> userManager, IUserService userServices)
        {
            /*_logger = logger;*/
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            /* _authService = authService;*/
            _userServices = userServices;
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            
        }

        public async Task<string> RegisterCustomer(SellerRegistrationRequest request)
        {

            /* _logger.LogInfo("Creating the Seller as a user first, before assigning the seller role to them and adding them to the Sellers table.");*/

            var user = await _userServices.RegisterUser(new UserRegistrationRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,

            });


            await _userManager.AddToRoleAsync(user, "Seller");

            var seller = new Seller
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserName = request.UserName,
                Address = request.Address,
                UserId = user.Id
            };

            await _sellerRepo.AddAsync(seller);
           

            /* var verificationToken = Guid.NewGuid().ToString();
             var emailSent = await _authService.SendVerificationEmail(request.Email, verificationToken);*/

            /* if (emailSent)
             {

                 user.VerificationToken = verificationToken;
                 await _userManager.UpdateAsync(user);
    */

            var result = new { success = true, message = "Registration Successful! Please login." };
            return JsonConvert.SerializeObject(result);


        }



      
    }
}

