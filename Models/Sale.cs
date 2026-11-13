using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }

    public int InventoryId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public DateTime SalesDate { get; set; }
    public int SaleDate { get; set; }
    public string ClientName { get; set; } = string.Empty;

    [ForeignKey(nameof(InventoryId))]
    public virtual Inventory Inventory { get; set; } = null!;
}