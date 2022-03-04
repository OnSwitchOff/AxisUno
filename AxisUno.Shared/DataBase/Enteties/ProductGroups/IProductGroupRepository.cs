using System.Threading.Tasks;

namespace AxisUno.DataBase.Enteties.ProductGroups;

public interface IProductGroupRepository
{
    public Task<ProductGroup> GetDefaultAsync();
}