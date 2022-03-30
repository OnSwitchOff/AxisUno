﻿using AxisUno.DataBase.My100REnteties.Partners;
using Microinvest.CommonLibrary.Enums;
using Microsoft.EntityFrameworkCore;

namespace AxisUno.DataBase.Repositories.Partners
{
    public partial class PartnerRepository : IPartnerRepository
    {
        /// <summary>
        /// Gets partner from the database by id.
        /// </summary>
        /// <param name="id">Id to search partner in the database.</param>
        /// <returns>Partner.</returns>
        /// <date>28.03.2022.</date>
        public Task<Partner?> GetPartnerById(int id)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                return databaseContext.Partners.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        /// <summary>
        /// Gets list of partners.
        /// </summary>
        /// <param name="status">Status of partner.</param>
        /// <returns>List of partners.</returns>
        /// <date>28.03.2022.</date>
        public IAsyncEnumerable<Partner> GetParners(ENomenclatureStatuses status)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                return databaseContext.Partners.Where(x => x.Status == status).Include(p => p.Group).AsAsyncEnumerable();
            }
        }
    }
}