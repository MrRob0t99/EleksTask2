using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleksTask;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public BasketRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(string userId, int productId)
        {
            var user = await _unitOfWork.Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return -1;
            }

            var product = await _unitOfWork.Context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return -1;
            }

            var basketProduct = new BasketProduct()
            {
                ApplicationUser = user,
                Product = product
            };
            await _unitOfWork.Context.BasketProducts.AddAsync(basketProduct);
            await _unitOfWork.Commit();
            return basketProduct.Id;
        }

        public async Task<bool> Delete(string userId, int productId)
        {
            var basket =
                await _unitOfWork.Context.BasketProducts.FirstOrDefaultAsync(b =>
                    b.ApplicationUserId == userId && b.ProductId == productId);

            if (basket != null)
            {
                _unitOfWork.Context.BasketProducts.Remove(basket);
                await _unitOfWork.Commit();
                return true;
            }

            return false;
        }

        public async Task<List<Product>> GetInfoProductAsync(string userId)
        {
            return await _unitOfWork.Context
                .BasketProducts
                .Where(bp => bp.ApplicationUserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product)
                .ToListAsync();
        }
    }
}
