using AxisUno.DataBase.Enteties.Partners;
using AxisUno.DataBase.Enteties.Payments;
using AxisUno.DataBase.Enteties.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Operations.Revaluations
{
    public class Revaluation : Operation
    {
        protected Revaluation(Partner partner, Payment payment, params OperationProduct[] operationProducts)
            : base(partner, payment, operationProducts)
        {
        }

        public override OperationType OperationType
        {
            get => OperationType.Revaluation;
        }
    }
}
