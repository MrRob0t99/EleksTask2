namespace EleksTask.Dto
{
    public class GetProductsRequestDto
    {
        public int CategoryId { get; set; }

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 20;

        public string Search { get; set; } = string.Empty;
    }
}
