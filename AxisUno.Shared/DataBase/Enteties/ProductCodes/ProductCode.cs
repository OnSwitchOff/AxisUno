using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.ProductCodes
{
    public class ProductCode : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        public ProductCode()
        {
        }

        public int Id { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public int ProductId { get; private set; }

        public string Measure { get; private set; } = string.Empty;

        public double Multiplier { get; private set; }
    }
}
