using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;


namespace EleksTask.Services
{
    public class ProductService : IProductService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductService(ApplicationContext context, IMapper mapper,IProductRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Response<int>> CreateProductAsync(int categoryId, CreateProductDto productDto)
        {
            var response = new Response<int>();
            var product = _mapper.Map<Product>(productDto);
            var category = await _repository.Add(categoryId, product);

            if (category==-1)
            {
                response.Error = new Error("Category not found");
                return response;
            }

            response.Data = product.Id;
            return response;
        }

        public async Task<Response<bool>> DeleteProductAsync(int productId)
        {
            var response = new Response<bool>();
            var result = await _repository.Delete(productId);
            if (!result)
            {
                response.Error = new Error("Product not found");
                return response;
            }
            response.Data = result;
            return response;
        }

        public async Task<Response<List<ProductDto>>> GetAllProducts()
        {
            var response = new Response<List<ProductDto>>();
            var products = await _repository.GetAll();
            response.Data = _mapper.Map<List<ProductDto>>(products);
            return response;
        }

        public async Task<Response<List<ProductDto>>> GetProductsByCategoryIdAsync(GetProductsRequestDto dto)
        {
            var response = new Response<List<ProductDto>>();
            var products = await _repository.GetProductsByCategoryId(dto.CategoryId, dto.Skip, dto.Take, dto.Search);
            response.Data = _mapper.Map<List<ProductDto>>(products);
            return response;
        }

        public async Task<Response<ProductDto>> GetProduct(int productId)
        {
            var response = new Response<ProductDto>();
            var product = await _repository.GetById(productId);
            if (product == null)
                response.Error = new Error("Product not found");
            else
                response.Data = _mapper.Map<ProductDto>(product);

            return response;
        }
    }
}

