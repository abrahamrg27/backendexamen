using System;
using backend.DTOs;
using FluentValidation;

namespace backend.Validators;

public class InventoryUpdateValidator : AbstractValidator<InventoryUpdateDto>
{
 public InventoryUpdateValidator()
    {
           RuleFor(x => x.InventoryId).NotEmpty().WithMessage("El Id es obligatorio").GreaterThan(0).WithMessage("El id debe ser mayo a 0");
         RuleFor(x => x.Quantity).NotEmpty().WithMessage("la cantidad debe ser mayor  0");
        RuleFor(x => x.BatchNumber).NotEmpty().WithMessage("El numero de lote  debe ser mayor  0");
        RuleFor(x => x.BeerId).NotNull().WithMessage("El identificador de las cervesas en obligatorio")
        .GreaterThan(0).WithMessage("Error con el valor de las cerveza");
        RuleFor(x => x.ProductionDate).NotEmpty().WithMessage("La fecha de produccion es obligatoria");
        RuleFor(x => x.PurchasePrice).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
        RuleFor(x => x.BestBeforeDate).NotEmpty().WithMessage("la fecha de producto es obligatoria")
        .GreaterThan(x => x.ProductionDate).WithMessage("la fecha de consmo preferente debe ser posterir ala actual");
    }
}
