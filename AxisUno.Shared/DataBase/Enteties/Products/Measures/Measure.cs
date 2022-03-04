using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products.Measures
{
    public record Measure : ValueObject
    {
        public Measure(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
