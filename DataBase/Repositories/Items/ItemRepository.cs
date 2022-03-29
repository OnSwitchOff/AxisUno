using AxisUno.DataBase;
using HarabaSourceGenerators.Common.Attributes;

namespace AxisUno.DataBase.Repositories.Items
{
    [Inject]
    public partial class ItemRepository : IItemRepository
    {
        private readonly DatabaseContext databaseContext;
    }
}
