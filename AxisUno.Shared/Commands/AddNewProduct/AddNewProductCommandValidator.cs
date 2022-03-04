using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Commands.AddNewProduct
{
    internal class AddNewProductCommandValidator : AbstractValidator<AddNewProductCommand>
    {
        public AddNewProductCommandValidator()
        {
            RuleFor(command => command.Name != null);
        }
    }
}
