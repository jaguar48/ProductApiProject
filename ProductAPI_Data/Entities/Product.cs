using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI_Data.Entities
{
   
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CreatedAt { get; set; } 
        public string UpdatedAt { get; set; }
        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }

        public Seller Seller { get; set; }
    }
}
