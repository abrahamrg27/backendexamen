using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class SaleRepository : IRepository<Sale>
{
    private readonly StoreContext _context;

    public SaleRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> Get() =>
        await _context.Sales.ToListAsync();

    public async Task<Sale> GetById(int id) =>
        await _context.Sales.FindAsync(id);

    public async Task Add(Sale entity) =>
        await _context.Sales.AddAsync(entity);

    public void Update(Sale entity)
    {
        _context.Sales.Attach(entity);
        _context.Sales.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Sale entity) =>
        _context.Sales.Remove(entity);

    public async Task Save() =>
        await _context.SaveChangesAsync();
}