using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private ApplicationContext _context;

        public BasketRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Add(string userId, int productId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return -1;
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return -1;
            }

            var basketProduct = new BasketProduct()
            {
                ApplicationUser = user,
                Product = product
            };
            await _context.BasketProducts.AddAsync(basketProduct);
            await _context.SaveChangesAsync();
            return basketProduct.Id;
        }

        public async Task<bool> Delete(string userId, int productId)
        {
            var basket =
                await _context.BasketProducts.FirstOrDefaultAsync(b =>
                    b.ApplicationUserId == userId && b.ProductId == productId);

            if (basket != null)
            {
                _context.BasketProducts.Remove(basket);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Product>> GetInfoProductAsync(string userId)
        {
            return await _context
                .BasketProducts
                .Where(bp => bp.ApplicationUserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product)
                .ToListAsync();
        }
    }
}
