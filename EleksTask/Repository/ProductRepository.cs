using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleksTask;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(int categoryId, Product entity)
        {
            var category = await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return -1;
            }

            entity.Category = category;
            await _unitOfWork.Context.Products.AddAsync(entity);
            await _unitOfWork.Commit();
            return entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _unitOfWork.Context.Products.FindAsync(id);
            if (existing != null)
            {
                _unitOfWork.Context.Products.Remove(existing);
                await _unitOfWork.Commit();
                return true;
            }

            return false;
        }

        public async Task<Product> GetById(int productId)
        {
            return await _unitOfWork.Context.Products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == productId);
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId, int skip, int take, string search)
        {
            return await _unitOfWork.Context.Products.AsNoTracking()
                .Where(p => p.CategoryId == categoryId && p.Name.Contains(search))
                .Skip(skip)
                .Take(take)
                .Select(pr => pr)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _unitOfWork.Context.Products.ToListAsync();
        }

        public async Task<bool> Rename(int categoryId, string newName)
        {
            var existing = await _unitOfWork.Context.Products.FindAsync(categoryId);
            if (existing != null)
            {
                existing.Name = newName;
                await _unitOfWork.Commit();
                return true;
            }

            return false;
        }
    }
}