using BuildingBlocks.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Commands.AddNewProduct
{
    public record AddNewProductCommand(string Name, string Code) : CommandBase<int>;
}
