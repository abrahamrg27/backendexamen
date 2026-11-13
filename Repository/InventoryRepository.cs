using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class InventoryRepository : IRepository<Inventory>
{
    private StoreContext _context;

    public InventoryRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventory>> Get() =>
        await _context.Inventories.ToListAsync();

    public async Task<Inventory> GetById(int id) =>
        await _context.Inventories.FindAsync(id);

    public async Task Add(Inventory entity) =>
        await _context.Inventories.AddAsync(entity);

    public void Update(Inventory entity)
    {
        _context.Inventories.Attach(entity);
        _context.Inventories.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Inventory entity) =>
        _context.Inventories.Remove(entity);

    public async Task Save() =>
        await _context.SaveChangesAsync();

    public async Task<IEnumerable<Inventory>> GetByBeerId(int beerId) =>
        await _context.Inventories.Where(i => i.BeerId == beerId).ToListAsync();

    public async Task<bool> ExistsByBatch(int batchNumber) =>
        await _context.Inventories.AnyAsync(i => i.BatchNumber == batchNumber);

    public async Task<bool> ExistsByBatchExcludingId(int batchNumber, int id) =>
        await _context.Inventories.AnyAsync(i => i.BatchNumber == batchNumber && i.InventoryId != id);
}
