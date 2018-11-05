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
        private readonly IBasketRepository _repository;

        public BasketService(ApplicationContext context, IMapper mapper, IBasketRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Response<bool>> AddProductToBasketAsync(string userId, int productId)
        {
            var response = new Response<bool>();
            var result = await _repository.Add(userId, productId);
            if (result == -1)
            {
                response.Error = new Error("Product or User not found");
                return response;
            }
            response.Data = true;
            return response;
        }

        public async Task<Response<BasketDto>> GetInfoProductAsync(string userId)
        {
            var response = new Response<BasketDto>();
            var productList = await _repository.GetInfoProductAsync(userId);
            response.Data = new BasketDto()
            {
                TotalPrice = productList.Sum(p => p.Price),
                Product = _mapper.Map<List<ProductDto>>(productList)
            };
            return response;
        }

        public async Task<Response<bool>> DeleteProductFromBasketAsync(string userId, int productId)
        {
            var response = new Response<bool>();
            var result = await _repository.Delete(userId, productId);
            if (!result)
            {
                response.Error = new Error("Not Found");
                return response;
            }
            response.Data = result;
            return response;
        }
    }
}

