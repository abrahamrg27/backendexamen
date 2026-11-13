using backend.DTOs;
using backend.Models;
using backend.Repository;

namespace backend.services;

public class InventoryService : ICommonServices<InventoryDto, InventoryInsertDto, InventoryUpdateDto>
{
    private readonly IRepository<Inventory> _inventoryRepository;

    public InventoryService(IRepository<Inventory> inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<InventoryDto>> Get()
    {
        var inventories = await _inventoryRepository.Get();
        return inventories.Select(i => new InventoryDto
        {
            InventoryId = i.InventoryId,
            Quantity = i.Quantity,
            BatchNumber = i.BatchNumber,
            BeerId = i.BeerId,
            ProductionDate = i.ProductionDate,
            PurchasePrice = i.PurchasePrice,
            StorageSection = i.StorageSection,
            BestBeforeDate = i.BestBeforeDate
        });
    }

    public async Task<InventoryDto> GetById(int id)
    {
        var inventory = await _inventoryRepository.GetById(id);

        if (inventory == null)
        {
            return null;
        }

        return new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            PurchasePrice = inventory.PurchasePrice,
            StorageSection = inventory.StorageSection,
            BestBeforeDate = inventory.BestBeforeDate
        };
    }

    public async Task<InventoryDto> Add(InventoryInsertDto inventoryInsertDto)
    {

        if (_inventoryRepository is InventoryRepository repo)
        {
            if (await repo.ExistsByBatch(inventoryInsertDto.BatchNumber))
            {
                return null;
            }
        }
        else
        {
            var inventories = await _inventoryRepository.Get();
            if (inventories.Any(i => i.BatchNumber == inventoryInsertDto.BatchNumber))
            {
                return null;
            }
        }

        var inventory = new Inventory
        {
            Quantity = inventoryInsertDto.Quantity,
            BatchNumber = inventoryInsertDto.BatchNumber,
            BeerId = inventoryInsertDto.BeerId,
            ProductionDate = inventoryInsertDto.ProductionDate,
            PurchasePrice = inventoryInsertDto.PurchasePrice,
            StorageSection = inventoryInsertDto.StorageSection,
            BestBeforeDate = inventoryInsertDto.BestBeforeDate
        };

        await _inventoryRepository.Add(inventory);
        await _inventoryRepository.Save();

        return new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            PurchasePrice = inventory.PurchasePrice,
            StorageSection = inventory.StorageSection,
            BestBeforeDate = inventory.BestBeforeDate
        };
    }

    public async Task<InventoryDto> Update(int id, InventoryUpdateDto inventoryUpdateDto)
    {
        var inventory = await _inventoryRepository.GetById(id);

        if (inventory == null)
        {
            return null;
        }

        if (_inventoryRepository is InventoryRepository repo)
        {
            if (await repo.ExistsByBatchExcludingId(inventoryUpdateDto.BatchNumber, id))
            {
                return null;
            }
        }
        else
        {
            var inventories = await _inventoryRepository.Get();
            if (inventories.Any(i => i.InventoryId != id && i.BatchNumber == inventoryUpdateDto.BatchNumber))
            {
                return null;
            }
        }

        inventory.Quantity = inventoryUpdateDto.Quantity;
        inventory.BatchNumber = inventoryUpdateDto.BatchNumber;
        inventory.BeerId = inventoryUpdateDto.BeerId;
        inventory.ProductionDate = inventoryUpdateDto.ProductionDate;
        inventory.PurchasePrice = inventoryUpdateDto.PurchasePrice;
        inventory.StorageSection = inventoryUpdateDto.StorageSection;
        inventory.BestBeforeDate = inventoryUpdateDto.BestBeforeDate;

        _inventoryRepository.Update(inventory);
        await _inventoryRepository.Save();

        return new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            PurchasePrice = inventory.PurchasePrice,
            StorageSection = inventory.StorageSection,
            BestBeforeDate = inventory.BestBeforeDate
        };
    }

    public async Task<InventoryDto> Delete(int id)
    {
        var inventory = await _inventoryRepository.GetById(id);

        if (inventory == null)
        {
            return null;
        }

        var inventoryDto = new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            PurchasePrice = inventory.PurchasePrice,
            StorageSection = inventory.StorageSection,
            BestBeforeDate = inventory.BestBeforeDate
        };

        _inventoryRepository.Delete(inventory);
        await _inventoryRepository.Save();

        return inventoryDto;
    }

    // Obtener inventario por cerveza
    public async Task<IEnumerable<InventoryDto>?> GetByBeerId(int beerId)
    {
        if (_inventoryRepository is InventoryRepository repo)
        {
            var inventories = await repo.GetByBeerId(beerId);
            return inventories.Select(i => new InventoryDto
            {
                InventoryId = i.InventoryId,
                Quantity = i.Quantity,
                BatchNumber = i.BatchNumber,
                BeerId = i.BeerId,
                ProductionDate = i.ProductionDate,
                PurchasePrice = i.PurchasePrice,
                StorageSection = i.StorageSection,
                BestBeforeDate = i.BestBeforeDate
            });
        }
        else
        {
            var inventories = await _inventoryRepository.Get();
            return inventories
                .Where(i => i.BeerId == beerId)
                .Select(i => new InventoryDto
                {
                    InventoryId = i.InventoryId,
                    Quantity = i.Quantity,
                    BatchNumber = i.BatchNumber,
                    BeerId = i.BeerId,
                    ProductionDate = i.ProductionDate,
                    PurchasePrice = i.PurchasePrice,
                    StorageSection = i.StorageSection,
                    BestBeforeDate = i.BestBeforeDate
                });
        }
    }
}
