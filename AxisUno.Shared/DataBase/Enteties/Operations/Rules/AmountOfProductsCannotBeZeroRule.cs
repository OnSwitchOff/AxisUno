using AxisUno.BusinessRules;
using AxisUno.DataBase.Enteties.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Operations.Rules
{
    internal class AmountOfProductsCannotBeZeroRule : IBusinessRule
    {
        private OperationProduct[] _operationProducts;

        public AmountOfProductsCannotBeZeroRule(OperationProduct[] operationProducts)
        {
            _operationProducts = operationProducts;
        }

        public string Message => "Amount of products can't be zero.";

        public bool BrokenWhen()
        {
            return _operationProducts.Length == 0;
        }
    }
}
