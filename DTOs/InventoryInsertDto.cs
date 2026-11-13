using System;

namespace backend.DTOs;

public class InventoryInsertDto
{
 public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public int BatchNumber { get; set; }

    public int BeerId { get; set; }

    public DateTime ProductionDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public string StorageSection { get; set; } 
    public DateTime BestBeforeDate { get; set; }
    public object SaleDate { get; internal set; }
}
