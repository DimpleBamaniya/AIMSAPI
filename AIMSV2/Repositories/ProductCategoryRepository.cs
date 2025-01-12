using AIMSV2.Data;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategoryDto> GetActiveProductCategories()
        {
            return _context.Categories
                .Where(c => c.IsActive)
                .Select(c => new ProductCategoryDto
                {
                    Id = c.ID,
                    Name = c.Name
                })
                .ToList();
        }

    }
}