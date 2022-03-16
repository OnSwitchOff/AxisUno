using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.PaymentTypes
{
    public partial class PaymentType : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Enums.PaymentIndex PaymentIndex { get; set; }
    }
}
