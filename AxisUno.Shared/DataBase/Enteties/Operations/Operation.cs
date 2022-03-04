using AxisUno.DataBase.Enteties.Operations.Events;
using AxisUno.DataBase.Enteties.Operations.Rules;
using AxisUno.DataBase.Enteties.Partners;
using AxisUno.DataBase.Enteties.Payments;
using AxisUno.DataBase.Enteties.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Operations
{
    public abstract class Operation : Entity
    {
        protected Operation(Partner partner, Payment payment, params OperationProduct[] operationProducts)
        {
            CheckRule(new AmountOfProductsCannotBeZeroRule(operationProducts));
            CheckRule(new QuantityCannotBeLessThanOneRule(operationProducts));

            Partner = partner;
            Payment = payment;
            OperationProducts = operationProducts;
        }

        public abstract OperationType OperationType { get; }

        public Partner Partner { get; }

        public Payment Payment { get; }

        public OperationProduct[] OperationProducts { get; }

        public int SourceDocumentId { get; private set; }

        protected virtual int Sign
        {
            get => 0;
        }

        //public static Operation Revaluation(Product product, Partner? partner = default, Payment? payment = default)
        //{
        //    var operationProduct = OperationProduct.FromProduct(product);
        //    //var operation = new Operation(OperationType.Revaluation, partner, payment, operationProduct);
        //    //return operation;
        //    //return new Operation();
        //}

        public virtual void Save()
        {
            AddDomainEvent(new SaveOperationEvent(this));
        }
    }
}
