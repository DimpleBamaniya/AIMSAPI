using AIMSV2.Data;
using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BrandDto> GetActiveBrands()
        {
            return _context.Brands
                .Where(c => c.IsActive)
                .Select(c => new BrandDto
                {
                    Id = c.ID,
                    Name = c.Name
                })
                .ToList();
        }

    }
}