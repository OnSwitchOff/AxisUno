using AxisUno.BusinessRules;
using AxisUno.DataBase.Enteties.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisUno.DataBase.Enteties.Operations.Rules
{
    internal class QuantityCannotBeLessThanOneRule : IBusinessRule
    {
        private OperationProduct[] _operationProducts;

        public QuantityCannotBeLessThanOneRule(OperationProduct[] operationProducts)
        {
            _operationProducts = operationProducts;
        }

        /// <inheritdoc/>
        public string Message => "Quantity can not be less, than one.";

        /// <inheritdoc/>
        public bool BrokenWhen()
        {
            return _operationProducts.Any(product => product.Quantity < 1);
        }
    }
}
