using AxisUno.DataBase.Enteties;

namespace AxisUno.DataBase.My100REnteties.ItemsGroups
{
    public partial class ItemsGroup : Entity
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Discount { get; set; }

        public List<Items.Item> Items { get; set; } = new List<Items.Item>();
    }
}
