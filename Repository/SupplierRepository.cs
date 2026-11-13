using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class SupplierRepository : IRepository<Supplier>
{
    private readonly StoreContext _context;

    public SupplierRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supplier>> Get() =>
        await _context.suppliers.ToListAsync();

    public async Task<Supplier> GetById(int id) =>
        await _context.suppliers.FindAsync(id);

    public async Task Add(Supplier entity) =>
        await _context.suppliers.AddAsync(entity);

    public void Update(Supplier entity)
    {
        _context.suppliers.Attach(entity);
        _context.suppliers.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Supplier entity) =>
        _context.suppliers.Remove(entity);

    public async Task Save() =>
        await _context.SaveChangesAsync();
}