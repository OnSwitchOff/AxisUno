// <copyright file="UpdateCompanyDataCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Commands.AppSettings.UpdateCompanyProperties
{
    using System.Threading;
    using System.Threading.Tasks;
    using AxisUno.Services.Settings;
    using BuildingBlocks.Application.Commands;
    using HarabaSourceGenerators.Common.Attributes;
    using MediatR;

    /// <summary>
    /// Describes logic of company data updating.
    /// </summary>
    [Inject]
    public partial class UpdateCompanyDataCommandHandler : ICommandHandler<UpdateCompanyDataCommand, bool>
    {
        private readonly ISettingsService settings;

        /// <summary>
        /// Updates data of company in the database.
        /// </summary>
        /// <param name="request">Parameters of UpdateCompanyDataCommand.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Returns 1 if data was updated successfully; otherwise returns 0.</returns>
        /// <date>24.03.2022.</date>
        Task<bool> IRequestHandler<UpdateCompanyDataCommand, bool>.Handle(UpdateCompanyDataCommand request, CancellationToken cancellationToken)
        {
            this.settings.AppSettings[Enums.ESettingKeys.Company].Value = request.companyData.Name;
            this.settings.AppSettings[Enums.ESettingKeys.Principal].Value = request.companyData.Principal;
            this.settings.AppSettings[Enums.ESettingKeys.City].Value = request.companyData.City;
            this.settings.AppSettings[Enums.ESettingKeys.Address].Value = request.companyData.Address;
            this.settings.AppSettings[Enums.ESettingKeys.Phone].Value = request.companyData.Phone;
            this.settings.AppSettings[Enums.ESettingKeys.BankName].Value = request.companyData.BankName;
            this.settings.AppSettings[Enums.ESettingKeys.BankBIC].Value = request.companyData.BankBIC;
            this.settings.AppSettings[Enums.ESettingKeys.IBAN].Value = request.companyData.IBAN;
            this.settings.AppSettings[Enums.ESettingKeys.TaxNumber].Value = request.companyData.TaxNumber;
            this.settings.AppSettings[Enums.ESettingKeys.VATNumber].Value = request.companyData.VATNumber;
            this.settings.AppSettings[Enums.ESettingKeys.OnlineShopNumber].Value = request.companyData.OnlineShopNumber;
            this.settings.AppSettings[Enums.ESettingKeys.OnlineShopDomainName].Value = request.companyData.OnlineShopDomainName;
            if (request.companyData.ShopType == null || request.companyData.ShopType.Value == null)
            {
                this.settings.AppSettings[Enums.ESettingKeys.OnlineShopType].Value = Microinvest.CommonLibrary.Enums.EOnlineShopTypes.OnlinePlatform.ToString();
            }
            else
            {
                this.settings.AppSettings[Enums.ESettingKeys.OnlineShopType].Value = request.companyData.ShopType.Value.ToString();
            }

            this.settings.UpdateSettings(Enums.ESettingGroups.App);

            return Task.FromResult(true);
        }
    }
}
