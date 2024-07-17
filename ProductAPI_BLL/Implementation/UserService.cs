
using Microsoft.AspNetCore.Identity;
using ProductAPI_BLL.Interface;
using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_BLL.Implementation
{

    public sealed class UserService : IUserService
    {


        private readonly UserManager<User> _userManager;



        public UserService(UserManager<User> userManager)
        {

            _userManager = userManager;
        }



        public async Task<User> RegisterUser(UserRegistrationRequest Request)
        {


            var existingUser = await _userManager.FindByEmailAsync(Request.Email.Trim().ToLower());
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email exists!");
            }

            var user = new User
            {
                FirstName = Request.FirstName,
                LastName = Request.LastName,
                UserName = Request.UserName,
                Email = Request.Email,


            };

            var result = await _userManager.CreateAsync(user, Request.Password);
            if (!result.Succeeded)
            {

                string errMsg = string.Join("\n", result.Errors.Select(x => x.Description));

                throw new InvalidOperationException($"Failed to create user:\n{errMsg}");
            }

            return user;

        }


    }
}
