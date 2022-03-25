// <copyright file="Extensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using AxisUno.Models;
    using Common.Enums;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.DeviceService.Models;

    /// <summary>
    /// Class with various extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts languages enum of the current app to languages enum of the PrinterService.
        /// </summary>
        /// <param name="language">Language of the current app.</param>
        /// <returns>Language of the PrinterService.</returns>
        /// <date>17.03.2022.</date>
        public static SupportedLanguagesEnum ConvertToSupportedLanguagesEnum(this ELanguages language)
        {
            if (Enum.IsDefined(typeof(SupportedLanguagesEnum), language.ToString()))
            {
                return (SupportedLanguagesEnum)Enum.Parse(typeof(SupportedLanguagesEnum), language.ToString());
            }
            else
            {
                return SupportedLanguagesEnum.Bulgarian;
            }
        }

        /// <summary>
        /// Fills up SaleProductModel with data from DataTable.
        /// </summary>
        /// <param name="products">
        /// Table to get data. Must include next necessary columns: Name, SalePrice, Quantity, Discount, VAT GroupID, VATGroupName, VATValue.
        /// </param>
        /// <returns>Fillled SaleProductModel list.</returns>
        /// <date>17.03.2022.</date>
        public static List<SaleProductModel> ParseToSaleProductModelList(this ObservableCollection<OperationItemModel> products)
        {
            List<SaleProductModel> sales = new List<SaleProductModel>();
            foreach (OperationItemModel item in products)
            {
                if (item.Item.Id > 0)
                {
                    sales.Add(item);
                }
            }

            return sales;
        }
    }
}
