using System.Collections.Generic;

namespace EleksTask.Dto
{
    public class BasketDto
    { 
        public List<ProductDto> Product { get; set; }

        public double TotalPrice { get; set; }
    }
}
