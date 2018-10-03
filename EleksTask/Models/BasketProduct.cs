namespace EleksTask.Models
{
    public class BasketProduct
    {
        public string ApolicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
