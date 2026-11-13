using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class SaleUpdateDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El InventoryId debe ser un valor positivo.")]
    public int InventoryId { get; set; }
     [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public int Quantity { get; set; }
    [Range(0.0, 100.0, ErrorMessage = "El precio debe estar entre 0.0 y 150.0.")]
    public decimal UnitPrice { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre del cliente debe tener entre 2 y 50 caracteres.")]
    public string ClientName {get; set; }

    public DateTime SalesDate { get; set; }
    public int SaleDate { get; set; }
    public DateTime BestBeforeDate { get; set; }

    
}