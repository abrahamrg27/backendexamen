using System;

namespace backend.DTOs;

public class SaleDto
{

    public int InventoryId { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int SaleDate { get; set; }
    public string ClientName {get; set; }

    
}