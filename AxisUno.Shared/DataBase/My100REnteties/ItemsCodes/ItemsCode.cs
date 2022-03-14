using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.ItemsCodes
{
    public partial class ItemsCode : Entity
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public Items.Item Item { get; set; }
        public string Measure { get; set; } = null!;
        public DateTime Multiplier { get; set; }
    }
}
