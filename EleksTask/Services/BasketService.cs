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
    public class BasketService : IBasketService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public BasketService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<bool>> AddProductToBasketAsync(string userId, int productId)
        {
            var response = new Response<bool>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Error = new Error("User not found");
                return response;
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                response.Error = new Error("Product not found");
                return response;
            }

            var basketProduct = new BasketProduct()
            {
                ApplicationUser = user,
                Product = product
            };
            await _context.BasketProducts.AddAsync(basketProduct);
            await _context.SaveChangesAsync();
            response.Data = true;
            return response;
        }

        public async Task<Response<BasketDto>> GetInfoProduct(string userId)
        {
            var response = new Response<BasketDto>();
            var productList = await _context
                .BasketProducts
                .Where(bp => bp.ApplicationUserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product)
                .ToListAsync();
            response.Data = new BasketDto()
            {
                TotalPrice = productList.Sum(p => p.Price),
                Product = _mapper.Map<List<ProductDto>>(productList)
            };
            return response;
        }
    }
}

