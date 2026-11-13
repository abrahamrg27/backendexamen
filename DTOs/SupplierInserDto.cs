using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class SupplierInsertDto
{
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Name { get; set; }
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Country { get; set; }
    [Range(7, 15, ErrorMessage = "El Numero debe tener de 7 a 15 digitos.")]
    public int Phone { get; set; }

    
}