using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products.Enums
{
    public enum ProductType : byte
    {
        Standard,

        /// <summary>
        /// Product with excise (fixed price)
        /// </summary>
        Excise,

        Work,

        Service,

        /// <summary>
        /// Lottery ticket
        /// </summary>
        LotteryTicket,

        /// <summary>
        /// Non inventoriable type of product
        /// TODO: Change name of the product type to more readable one.
        /// </summary>
        Payment,

        Other,
    }
}
