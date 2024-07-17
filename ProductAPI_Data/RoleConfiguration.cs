using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_Data
{
  
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Name = "Seller",
                    NormalizedName = "SELLER"
                }

            );
        }
        public void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {


            if (!roleManager.RoleExistsAsync("Seller").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Seller",
                    NormalizedName = "SELLER"
                };

                var result = roleManager.CreateAsync(role).Result;
            }


        }

    }
}
