using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.Serializations
{
    public partial class Serialization : Entity
    {
        public int Id { get; set; }
        public string Group { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
