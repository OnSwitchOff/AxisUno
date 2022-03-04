using AxisUno.DataBase.Enteties.ProductGroups;
using AxisUno.DataBase.Enteties.Products.Enums;
using AxisUno.DataBase.Enteties.Products.Measures;
using AxisUno.DataBase.Enteties.Products.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products
{
    public class Product : Entity
    {
        private Product(
            string name,
            string? code,
            string? barcode,
            ProductType type,
            ProductStatus status)
        {
            Name = name;
            Barcode = barcode;
            Code = code;
            Type = type;
            Status = status;
            Measure = default!;
            ProductGroup = default!;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Gets additional identifier of the product, using for "sale by code" rather, than barcode.
        /// </summary>
        public string? Code { get; private set; }

        public string? Barcode { get; private set; }

        public ProductType Type { get; private set; }

        public ProductStatus Status { get; private set; }

        public Measure Measure { get; private set; }

        public ProductGroup ProductGroup { get; private set; }

        public static Product Create(
            string name,
            ProductGroup productGroup,
            Measure measure,
            string? code = null,
            string? barcode = null,
            ProductType type = ProductType.Standard,
            ProductStatus status = ProductStatus.Active)
        {
            CheckRule(new NameMustNotBeEmptyRule(name));

            var product = new Product(name, code, barcode, type, status)
            {
                Measure = measure,
                ProductGroup = productGroup,
            };

            return product;
        }
    }
}
