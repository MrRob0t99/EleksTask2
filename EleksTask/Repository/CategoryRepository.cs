using System.Collections.Generic;
using System.Threading.Tasks;
using EleksTask;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EleksTask.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(Category entity)
        {
            if (await _unitOfWork.Context.Categories.AnyAsync(c => c.Name == entity.Name))
            {
                return -1;
            }

            await _unitOfWork.Context.Categories.AddAsync(entity);
            await _unitOfWork.Commit();
            return entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _unitOfWork.Context.Categories.FindAsync(id);
            if (existing != null)
            {
                _unitOfWork.Context.Categories.Remove(existing);
                await _unitOfWork.Commit();
                return true;
            }

            return false;
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _unitOfWork.Context.Categories.ToListAsync();
        }

        public async Task<bool> Rename(int categoryId, string newName)
        {
            var existing = await _unitOfWork.Context.Categories.FindAsync(categoryId);
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