// <copyright file="UpdateCompanyDataCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Commands.AppSettings.UpdateCompanyProperties
{
    using AxisUno.Models;
    using BuildingBlocks.Application.Commands;

    /// <summary>
    /// Updates data of company in the database.
    /// </summary>
    /// <param name="companyData">Data of company.</param>
    /// <date>24.03.2022.</date>
    public record class UpdateCompanyDataCommand(CompanyModel companyData) : CommandBase<bool>;
}
