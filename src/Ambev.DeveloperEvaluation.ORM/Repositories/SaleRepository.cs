using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sale sale)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                                 .Include(s => s.Items)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<(IEnumerable<Sale> Sales, int TotalItems)> ListPagedAsync(int page, int pageSize)
        {
            var query = _context.Sales
                .AsNoTracking()
                .Include(s => s.Items)
                .OrderByDescending(s => s.Date);

            var totalItems = await query.CountAsync();

            var sales = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (sales, totalItems);
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}
