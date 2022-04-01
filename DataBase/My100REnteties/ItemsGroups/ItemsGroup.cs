using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.Interfaces;

namespace AxisUno.DataBase.My100REnteties.ItemsGroups
{
    public partial class ItemsGroup : Entity, INomenclaturesGroups
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Discount { get; set; }

        public List<Items.Item> Items { get; set; } = new List<Items.Item>();
    }
}
