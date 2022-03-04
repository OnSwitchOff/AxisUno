using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Stores
{
    public class Store : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        private Store()
        {
        }

        public int Id { get; private set; }

        public int OperType { get; private set; }

        public int SrcId { get; private set; }

        public int ProductId { get; private set; }

        public decimal Qtty { get; private set; }

        public decimal Price { get; private set; }

        public DateTime Date { get; private set; }
    }
}
