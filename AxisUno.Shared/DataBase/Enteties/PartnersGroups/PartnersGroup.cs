using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.PartnersGroups
{
    public class PartnersGroup : Entity
    {
        /// <summary>
        /// This constructor is needed only for EF Core.
        /// </summary>
        private PartnersGroup()
        {
        }

        public int Id { get; private set; }

        public string Path { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        public int Discount { get; private set; }
    }
}
