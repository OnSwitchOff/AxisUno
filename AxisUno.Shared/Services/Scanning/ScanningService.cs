// <copyright file="ScanningService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Scanning
{
    using AxisUno.Services.Settings;
    using HarabaSourceGenerators.Common.Attributes;
    using Microinvest.DeviceService;

    /// <summary>
    /// Describes scanning service.
    /// </summary>
    // [Inject]
    public partial class ScanningService : IScanningData
    {
        private readonly ISettingsService settingsService;
        private COMScannerService comScanner;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanningService"/> class.
        /// </summary>
        /// <param name="settingsService">Service to get data of settings.</param>
        public ScanningService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
            this.comScanner = new COMScannerService();
            this.comScanner.SendScannedBarcode += this.SendScannedBarcode;
        }

        /// <summary>
        /// SendScannedData event.
        /// </summary>
        /// <date>16.03.2022.</date>
        public event SendScannedBarcodeDelegate SendScannedDataEvent;

        /// <summary>
        /// Starts listening of COM port.
        /// </summary>
        /// <param name="cOMPort">COM port to listen.</param>
        //// <date>16.03.2022.</date>
        public void StartCOMScanner(string cOMPort)
        {
            try
            {
                this.comScanner?.StartComPortListener(cOMPort);
            }
            catch
            {
                this.settingsService.COMScannerSettings[Enums.ESettingKeys.ComPort].Value = string.Empty;
                this.settingsService.UpdateSettings(Enums.ESettingGroups.COMScanner);
            }
        }

        /// <summary>
        /// Stop listenin of COM port.
        /// </summary>
        //// <date>16.03.2022.</date>
        public void StopCOMScanner()
        {
            this.comScanner?.StopComPortListener();
        }

        /// <summary>
        /// Invoke SendScannedData event if data was received.
        /// </summary>
        /// <param name="barcode">Got data.</param>
        //// <date>16.03.2022.</date>
        private void SendScannedBarcode(string barcode)
        {
            if (this.SendScannedDataEvent != null)
            {
                this.SendScannedDataEvent.Invoke(barcode);
            }
        }
    }
}
