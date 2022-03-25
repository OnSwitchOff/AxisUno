// <copyright file="SettingsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AxisUno.Enums;
    using HarabaSourceGenerators.Common.Attributes;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.DeviceService.CustomTypes;

    /// <summary>
    /// Describes setting service.
    /// </summary>
    // [Inject]
    public partial class SettingsService : ISettingsService
    {
        private CultureInfo culture;
        private char decimalSeparator;
        private string priceFormat;
        private string qtyFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        public SettingsService()
        {
            culture = CultureInfo.CurrentCulture;
            priceFormat = String.Concat("N", culture.NumberFormat.CurrencyDecimalDigits);
            qtyFormat = String.Concat("N", culture.NumberFormat.NumberDecimalDigits);

            decimalSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            InitSettingsFromDatabase();
        }

        /// <summary>
        /// Gets culture used on the PC.
        /// </summary>
        /// <date>16/03/2022.</date>
        public CultureInfo Culture
        {
            get => culture;
        }

        /// <summary>
        /// Decimal separator
        /// </summary>
        /// <developer>Serhii Rozniuk</developer>
        /// <date>24.02.2020</date>
        public char DecimalSeparator
        {
            get => decimalSeparator;
        }

        /// <summary>
        /// Gets price format (number of digits after decimal point).
        /// </summary>
        /// <date>16/03/2022.</date>
        public string PriceFormat
        {
            get => this.priceFormat;
        }

        /// <summary>
        /// Gets quantity format (number of digits after decimal point).
        /// </summary>
        /// <date>16/03/2022.</date>
        public string QtyFormat
        {
            get => this.qtyFormat;
        }

        /// <summary>
        /// Path to company logo image.
        /// </summary>
        /// <date>18/03/2022.</date>
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets language of the application.
        /// </summary>
        /// <date>16/03/2022.</date>
        public ELanguages AppLanguage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets or sets country for which the app was installed.
        /// </summary>
        /// <date>16/03/2022.</date>
        public ECountries Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets supported languages.
        /// </summary>
        /// <date>16/03/2022.</date>
        public List<ELanguages> SupportedLanguages => throw new NotImplementedException();

        /// <summary>
        /// Gets supported countries.
        /// </summary>
        /// <date>16/03/2022.</date>
        public List<ECountries> SupportedCountries => throw new NotImplementedException();

        /// <summary>
        /// Gets settings of a fiscal printer.
        /// </summary>
        /// <date>16/03/2022.</date>
        public Dictionary<ESettingKeys, SettingsItemModel> FiscalPrinterSettings => throw new NotImplementedException();

        /// <summary>
        /// Gets settings of a POS-terminal.
        /// </summary>
        /// <date>16/03/2022.</date>
        public Dictionary<ESettingKeys, SettingsItemModel> POSTerminalSettings => throw new NotImplementedException();

        /// <summary>
        /// Gets settings of a COM scanner.
        /// </summary>
        /// <date>16/03/2022.</date>
        public Dictionary<ESettingKeys, SettingsItemModel> COMScannerSettings => throw new NotImplementedException();

        /// <summary>
        /// Gets settings of the application.
        /// </summary>
        /// <date>16/03/2022.</date>
        public Dictionary<ESettingKeys, SettingsItemModel> AppSettings => throw new NotImplementedException();

        /// <summary>
        /// Gets settings of an Axis-cloud.
        /// </summary>
        /// <date>16/03/2022.</date>
        public Dictionary<ESettingKeys, SettingsItemModel> AxisCloudSettings => throw new NotImplementedException();

        /// <summary>
        /// Gets or sets unique sale number (will be printed on the receipt).
        /// </summary>
        /// <date>16/03/2022.</date>
        public UniqueSaleNumber UniqueSaleNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Init settings dictionaries.
        /// </summary>
        /// <date>16/03/2022.</date>
        private void InitSettingsFromDatabase()
        {

        }

        /// <summary>
        /// Update settings in the database.
        /// </summary>
        /// <param name="settingsGroup">Group of settings to update.</param>
        /// <date>16/03/2022.</date>
        public void UpdateSettings(ESettingGroups settingsGroup)
        {
            throw new NotImplementedException();
        }
    }
}
