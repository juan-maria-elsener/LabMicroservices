using Customer.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Validators
{
    public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido.");

            // Validamos los nuevos campos
            RuleFor(x => x.Street).NotEmpty().WithMessage("La calle es obligatoria.");
            RuleFor(x => x.City).NotEmpty().WithMessage("La ciudad es obligatoria.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("El país es obligatorio.");
        }
    }
}
