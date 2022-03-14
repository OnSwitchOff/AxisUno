using System;
using System.Collections.Generic;
using System.Text;
using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.PartnersGroups
{
    public partial class PartnersGroup : Entity
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Discount { get; set; }

        public List<Partners.Partner> Partners { get; set; } = new List<Partners.Partner>();
    }
}
