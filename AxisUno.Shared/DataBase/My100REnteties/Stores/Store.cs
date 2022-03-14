using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.Stores
{
    public partial class Store : Entity
    {
        public int Id { get; set; }
        public Enums.StoreOperType OperType { get; set; }
        public OperationDetails.OperationDetail Src { get; set; }
        public Items.Item Item { get; set; }
        public decimal Qtty { get; set; }
        public decimal Price { get; set; }
        public decimal Date { get; set; }
    }
}
