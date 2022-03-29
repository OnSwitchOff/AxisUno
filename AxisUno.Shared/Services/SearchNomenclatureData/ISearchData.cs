// <copyright file="ISearchData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.SearchNomenclatureData
{
    using System.Threading.Tasks;
    using AxisUno.Models;
    using Microinvest.CommonLibrary.Enums;

    /// <summary>
    /// Describes service to search data of nomenclature.
    /// </summary>
    public interface ISearchData
    {
        /// <summary>
        /// Initialize connection to Microinvest club to search items and partners by key.
        /// </summary>
        /// <param name="language">Used application language (will be used to search data in according to language).</param>
        void InitializeSearchDataTool(ELanguages language);

        /// <summary>
        /// Search partner data on the Microinvest club service by tax number or VAT number.
        /// </summary>
        /// <param name="searchKey">Partner tax number or VAT number.</param>
        /// <returns>Returns the filled partner model if the search was successful; otherwise returns the partner model with default data.</returns>
        /// <date>17.03.2022.</date>
        Task<PartnerModel> GetPartnerData(string searchKey);

        /// <summary>
        /// Search product data on the Microinvest club service by barcode.
        /// </summary>
        /// <param name="barcode">Product barcode.</param>
        /// <returns>Returns the filled product model if the search was successful; otherwise returns the product model with default data.</returns>
        /// <date>17.03.2022.</date>
        Task<ItemModel> GetItemData(string barcode);
    }
}
