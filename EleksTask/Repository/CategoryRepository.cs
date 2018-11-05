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
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Category entity)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == entity.Name))
            {
                return -1;
            }

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing != null)
            {
                _context.Categories.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> Rename(int categoryId, string newName)
        {
            var existing = await _context.Categories.FindAsync(categoryId);
            if (existing != null)
            {
                existing.Name = newName;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}