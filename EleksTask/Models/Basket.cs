using System.Collections.Generic;

namespace EleksTask.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public List<BasketProduct> Products { get; set; } = new List<BasketProduct>();

        public ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
