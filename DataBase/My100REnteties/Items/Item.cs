using AxisUno.DataBase.My100REnteties.Items.Enums;
using AxisUno.DataBase.My100REnteties.ItemsGroups;
using AxisUno.DataBase.My100REnteties.Vatgroups;
using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.Items
{
    public partial class Item : Entity
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public string Measure { get; set; } = null!;
        public ItemsGroup Group { get; set; }
        public Vatgroup Vatgroup { get; set; }
        public ItemType ItemType { get; set; }
        public ItemStatus Status { get; set; }

        public List<ItemsCodes.ItemsCode> ItemsCodes { get; set; }= new List<ItemsCodes.ItemsCode>();

    }
}
