using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.Vatgroups
{
    public partial class Vatgroup: Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Vatvalue { get; set; }
    }
}
