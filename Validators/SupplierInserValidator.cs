using System;
using backend.DTOs;
using FluentValidation;

namespace backend.Validators;

public class SupplierValidator : AbstractValidator<SupplierInsertDto>
{
    public SupplierValidator()
    {
        
        RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe ser de 2 a 20 caracteres");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("El numero debe tener de 7 a 15 digitos ");
        RuleFor(x => x.Country).Length(2, 20).WithMessage("El nombre debe ser de 2 a 20 caracteres");
    }
}