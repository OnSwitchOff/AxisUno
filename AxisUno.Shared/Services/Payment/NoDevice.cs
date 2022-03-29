// <copyright file="NoDevice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Payment
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using AxisUno.Models;
    using AxisUno.Services.Settings;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.DeviceService;
    using Microinvest.DeviceService.Helpers;

    /// <summary>
    /// Describes payment service.
    /// </summary>
    internal class NoDevice : IDevice
    {
        private ISettingsService settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoDevice"/> class.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        public NoDevice(ISettingsService settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Gets number of receipt.
        /// </summary>
        /// <date>17.03.2022.</date>
        public string ReceiptNumber
        {
            get => FiscalPrinterService.GetDefaultReceiptNumber(
                this.settings.FiscalPrinterSettings[Enums.ESettingKeys.OperatorCode],
                this.settings.UniqueSaleNumber);
        }

        /// <summary>
        /// Gets serial number of fiscal device.
        /// </summary>
        /// <date>17.03.2022.</date>
        public string FiscalPrinterSerialNumber
        {
            get => string.Empty;
        }

        /// <summary>
        /// Gets memory number of fiscal device.
        /// </summary>
        /// <date>17.03.2022.</date>
        public string FiscalPrinterMemoryNumber
        {
            get => string.Empty;
        }

        /// <summary>
        /// Initialize settings of device.
        /// </summary>
        /// <param name="settings">Application settings.</param>
        /// <date>17.03.2022.</date>
        public void InitializeDeviceSettings(ISettingsService settings)
        {
        }

        /// <summary>
        /// Pay order by POS terminal (if it is used) and print fiscal receipt.
        /// </summary>
        /// <param name="products">Products list to sale.</param>
        /// <param name="paymentType">Payment type.</param>
        /// <param name="receivedCash">Amount of money has paid by customer.</param>
        /// <returns>
        /// Returns FiscalExecutionResult with:
        /// - empty ResultException and AdditionalData properties if the print receipt has been success;
        /// - initialized ResultException and AdditionalData (DialogResult.Abort/DialogResult.Ignore) properties if the print receipt has been unsuccess.
        /// </returns>
        /// <date>17.03.2022.</date>
        public Task<FiscalExecutionResult> PayOrderAsync(ObservableCollection<OperationItemModel> products, EPaymentTypes paymentType, decimal receivedCash)
        {
            return Task.FromResult(new FiscalExecutionResult());
        }
    }
}
