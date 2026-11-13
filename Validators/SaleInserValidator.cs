using System;
using backend.DTOs;
using FluentValidation;

namespace backend.Validators;

public class SaleInserValidator : AbstractValidator<SaleInserDto>
{
    public SaleInserValidator()
    {
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("la cantidad debe ser mayor  0");
        RuleFor(x => x.InventoryId).NotNull().WithMessage("El identificador de las inventario en obligatorio")
        .GreaterThan(0).WithMessage("Error con el valor la cantidad debe ser mayor a 0");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("La fecha de venta es obligatoria");
        RuleFor(X => X.UnitPrice).NotEmpty().WithMessage("el precio debe ser mayor que 0");
    }
}