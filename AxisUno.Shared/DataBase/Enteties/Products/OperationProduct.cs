using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products
{
    public class OperationProduct
    {
        public OperationProduct(Product product, int quantity, decimal salePrice, decimal purchasePrice)
        {
            Product = product;
            Quantity = quantity;
            SalePrice = salePrice;
            PurchasePrice = purchasePrice;
        }

        public Product Product { get; private set; }

        public int Quantity { get; private set; }

        public decimal SalePrice { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public static OperationProduct FromProduct(Product product)
        {
            return new OperationProduct(product, 0, 0, 0);
        }
    }
}
