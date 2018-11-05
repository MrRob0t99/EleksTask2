using System.Collections.Generic;

namespace EleksTask.Dto
{
    public class GetCategoryDto : GetAllCategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
