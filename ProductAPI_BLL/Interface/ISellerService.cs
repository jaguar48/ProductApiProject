using ProductAPI_Data.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_BLL.Interface
{
    public interface ISellerService
    {
        Task<string> RegisterCustomer(SellerRegistrationRequest request);
    }
}
