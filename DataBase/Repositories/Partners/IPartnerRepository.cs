using AxisUno.DataBase.My100REnteties.Partners;
using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.Repositories.Partners
{
    public interface IPartnerRepository
    {
        /// <summary>
        /// Gets partner from the database by id.
        /// </summary>
        /// <param name="id">Id to search partner in the database.</param>
        /// <returns>Partner.</returns>
        /// <date>28.03.2022.</date>
        Task<Partner?> GetPartnerById(int id);

        /// <summary>
        /// Gets list of partners.
        /// </summary>
        /// <param name="status">Status of partner.</param>
        /// <returns>List of partners.</returns>
        /// <date>28.03.2022.</date>
        IAsyncEnumerable<Partner> GetParners(ENomenclatureStatuses status = ENomenclatureStatuses.Active);
    }
}
