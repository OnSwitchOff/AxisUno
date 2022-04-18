using Microinvest.CommonLibrary.Enums;

namespace AxisUno.DataBase.Repositories.OperationHeader
{
    public partial class OperationHeaderRepository : IOperationHeaderRepository
    {
        /// <summary>
        /// Get next acct to the concrete operation.
        /// </summary>
        /// <param name="operType">Operation type for which is needed to find next account number.</param>
        /// <returns>Next acc.</returns>
        /// <date>13.04.2022.</date>
        public Task<int> GetNextAcctAsync(EOperTypes operType)
        {
            return Task.Run(() =>
            {
                return 0;
            });
        }

        /// <summary>
        /// Get next unique sale number.
        /// </summary>
        /// <param name="fiscalDeviceNumber">Number of a fiscal device for which is needed to find next unique sale number.</param>
        /// <returns>Next unique sale number.</returns>
        /// <date>13.04.2022.</date>
        public Task<int> GetNextSaleNumberAsync(string fiscalDeviceNumber)
        {
            return Task.Run(() =>
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    return dbContext.OperationHeaders.Where(oh => oh.OperType == EOperTypes.Sale && oh.Usn.Equals(fiscalDeviceNumber)).Max(oh=> oh.EcrreceiptNumber);
                }
            });
        }
    }
}
