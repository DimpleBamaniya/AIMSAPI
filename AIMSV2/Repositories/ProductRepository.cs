using AIMSV2.Data;
using AIMSV2.Models;
using AIMSV2.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AIMSV2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}