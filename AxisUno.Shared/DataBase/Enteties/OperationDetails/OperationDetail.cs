using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.OperationDetails
{
    public class OperationDetail : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        private OperationDetail()
        {
        }

        public int Id { get; private set; }

        public int OperationHeaderId { get; private set; }

        public int ProductId { get; private set; }

        public decimal Qtty { get; private set; }

        public int Sign { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SalePrice { get; private set; }

        public decimal PurchaseVAT { get; private set; }

        public decimal SaleVAT { get; private set; }

        public int SrcId { get; private set; }
    }
}
