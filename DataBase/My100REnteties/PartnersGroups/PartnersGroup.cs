using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.Interfaces;

namespace AxisUno.DataBase.My100REnteties.PartnersGroups
{
    public partial class PartnersGroup : Entity, INomenclaturesGroups
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Discount { get; set; }

        public List<Partners.Partner> Partners { get; set; } = new List<Partners.Partner>();
    }
}
