using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.EntityFrameworkCore;


namespace EleksTask.Services
{
    public class ProductService : IProductService
    {

        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateProductAsync(int categoryId, CreateProductDto productDto)
        {
            var response = new Response<int>();
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                response.Error = new Error("Category not found");
                return response;
            }
            var product = _mapper.Map<Product>(productDto);
            product.Category = category;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            response.Data = product.Id;
            return response;
        }

        public async Task<Response<bool>> DeleteProductAsync(int productId)
        {
            var response = new Response<bool>();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                response.Error = new Error("Product not found");
                return response;
            }
            _context.Remove(product);
            await _context.SaveChangesAsync();
            response.Data = true;
            return response;
        }

        public async Task<Response<List<ProductDto>>> GetAllProducts()
        {
            var response = new Response<List<ProductDto>>();
            var products = await _context.Products.AsNoTracking().ToListAsync();
            response.Data = _mapper.Map<List<ProductDto>>(products);
            return response;
        }

        public async Task<Response<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var response = new Response<List<ProductDto>>();
            var products = await _context
               .Products
               .AsNoTracking()
               .Where(p => p.CategoryId == categoryId)
               .Select(pr => pr)
               .ToListAsync();
            response.Data = _mapper.Map<List<ProductDto>>(products);
            return response;
        }

        public async Task<Response<ProductDto>> GetProduct(int productId)
        {
            var response = new Response<ProductDto>();
            var product = await _context.Products.AsNoTracking().FirstAsync(p => p.Id == productId);
            if (product == null)
                response.Error = new Error("Product not found");
            else
                response.Data = _mapper.Map<ProductDto>(product);

            return response;
        }
    }
}

