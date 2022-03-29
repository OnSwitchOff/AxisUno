// <copyright file="SettingsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using AxisUno.Models;
    using AxisUno.Services.Settings;
    using Common.Enums;
    using Common.Interfaces;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.DeviceService.Helpers;
    using Microinvest.DeviceService.Models;
    using PinPadService.Interfaces;
    using PrinterService.Interfaces;

    /// <summary>
    /// Describes data for SettingsView.
    /// </summary>
    public partial class SettingsViewModel : ObservableObject
    {
        private ISettingsService settings;
        private CompanyModel company;
        private Image companyLogo;
        private ObservableCollection<ComboBoxItemModel> onlineShopTypes;
        private string uSN;
        private int operatorCode;
        private string operatorName;
        private ObservableCollection<ComboBoxItemModel> languages;
        private ComboBoxItemModel selectedLanguage;

        private ObservableCollection<string> cOMPorts;
        private ObservableCollection<int> baudRates;
        private ObservableCollection<ComboBoxItemModel> fiscalPrinterManufacturers;
        private ObservableCollection<ComboBoxItemModel> fiscalPrinterModels;
        private ObservableCollection<ComboBoxItemModel> fiscalPrinterProtocols;
        private ObservableCollection<ComboBoxItemModel> pOSTerminalManufacturers;
        private ObservableCollection<ComboBoxItemModel> pOSTerminalModels;
        private ObservableCollection<ComboBoxItemModel> pOSTerminalProtocols;

        private string selectedScannerCOMPort;
        private bool fiscalPrinterIsUsed;
        private DeviceSettingsModel fiscalPrinter;
        private bool pOSTerminalIsUsed;
        private DeviceSettingsModel pOSTerminal;
        private bool axisCloudIsUsed;
        private DeviceSettingsModel axisCloud;

        private string licenseCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settings">Settings of the application.</param>
        public SettingsViewModel(ISettingsService settings)
        {
            this.settings = settings;

            this.SetCompanyData();
            this.SetApplicationProperties();
            this.SetScannerProperties();
            this.SetDeviceProperties();
            this.SetAxisCloudProperties();
        }

        /// <summary>
        /// Gets or sets data of our company.
        /// </summary>
        /// <date>23.03.2022.</date>
        public CompanyModel Company
        {
            get => this.company;
            set => this.SetProperty(ref this.company, value);
        }

        /// <summary>
        /// Gets or sets logo of our company.
        /// </summary>
        /// <date>23.03.2022.</date>
        public Image CompanyLogo
        {
            get => this.companyLogo;
            set => this.SetProperty(ref this.companyLogo, value);
        }

        /// <summary>
        /// Gets or sets list with types of online-shops.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> OnlineShopTypes
        {
            get => this.onlineShopTypes;
            set => this.SetProperty(ref this.onlineShopTypes, value);
        }

        /// <summary>
        /// Gets or sets unique sale number.
        /// </summary>
        /// <date>23.03.2022.</date>
        public string USN
        {
            get => this.uSN;
            set => this.SetProperty(ref this.uSN, value);
        }

        /// <summary>
        /// Gets or sets operator code that will be used to create an unique sale number in the receipt.
        /// </summary>
        /// <date>23.03.2022.</date>
        public int OperatorCode
        {
            get => this.operatorCode;
            set => this.SetProperty(ref this.operatorCode, value);
        }

        /// <summary>
        /// Gets or sets the name of the operator who works with this app.
        /// </summary>
        /// <date>23.03.2022.</date>
        public string OperatorName
        {
            get => this.operatorName;
            set => this.SetProperty(ref this.operatorName, value);
        }

        /// <summary>
        /// Gets or sets list with supported languages.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> Languages
        {
            get => this.languages;
            set => this.SetProperty(ref this.languages, value);
        }

        /// <summary>
        /// Gets or sets language of the application.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ComboBoxItemModel SelectedLanguage
        {
            get => this.selectedLanguage;
            set => this.SetProperty(ref this.selectedLanguage, value);
        }

        /// <summary>
        /// Gets or sets list with supported COM ports.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<string> COMPorts
        {
            get => this.cOMPorts;
            set => this.SetProperty(ref this.cOMPorts, value);
        }

        /// <summary>
        /// Gets or sets list with supported baud rates.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<int> BaudRates
        {
            get => this.baudRates;
            set => this.SetProperty(ref this.baudRates, value);
        }

        /// <summary>
        /// Gets or sets list with supported manufacturers of a fiscal printer.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> FiscalPrinterManufacturers
        {
            get => this.fiscalPrinterManufacturers;
            set => this.SetProperty(ref this.fiscalPrinterManufacturers, value);
        }

        /// <summary>
        /// Gets or sets list with supported models of a fiscal printer.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> FiscalPrinterModels
        {
            get => this.fiscalPrinterModels;
            set => this.SetProperty(ref this.fiscalPrinterModels, value);
        }

        /// <summary>
        /// Gets or sets list with supported protocols of the fiscal printer.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> FiscalPrinterProtocols
        {
            get => this.fiscalPrinterProtocols;
            set => this.SetProperty(ref this.fiscalPrinterProtocols, value);
        }

        /// <summary>
        /// Gets or sets list with supported manufacturers of a POS-terminal.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> POSTerminalManufacturers
        {
            get => this.pOSTerminalManufacturers;
            set => this.SetProperty(ref this.pOSTerminalManufacturers, value);
        }

        /// <summary>
        /// Gets or sets list with supported models of a POS-terminal.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> POSTerminalModels
        {
            get => this.pOSTerminalModels;
            set => this.SetProperty(ref this.pOSTerminalModels, value);
        }

        /// <summary>
        /// Gets or sets list with supported protocols of the POS-terminal.
        /// </summary>
        /// <date>23.03.2022.</date>
        public ObservableCollection<ComboBoxItemModel> POSTerminalProtocols
        {
            get => this.pOSTerminalProtocols;
            set => this.SetProperty(ref this.pOSTerminalProtocols, value);
        }

        /// <summary>
        /// Gets or sets COM port of scanner is selected by user.
        /// </summary>
        /// <date>23.03.2022.</date>
        public string SelectedScannerCOMPort
        {
            get => this.selectedScannerCOMPort;
            set => this.SetProperty(ref this.selectedScannerCOMPort, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether fiscal printer is used.
        /// </summary>
        /// <date>23.03.2022.</date>
        public bool FiscalPrinterIsUsed
        {
            get => this.fiscalPrinterIsUsed;
            set => this.SetProperty(ref this.fiscalPrinterIsUsed, value);
        }

        /// <summary>
        /// Gets or sets a properties of a fiscal printer.
        /// </summary>
        /// <date>23.03.2022.</date>
        public DeviceSettingsModel FiscalPrinter
        {
            get => this.fiscalPrinter;
            set
            {
                if (this.fiscalPrinter != null)
                {
                    this.fiscalPrinter.PropertyChanged -= this.FiscalPrinter_PropertyChanged;
                }

                this.SetProperty(ref this.fiscalPrinter, value);
                this.fiscalPrinter.PropertyChanged += this.FiscalPrinter_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether POS-terminal is used.
        /// </summary>
        /// <date>23.03.2022.</date>
        public bool POSTerminalIsUsed
        {
            get => this.pOSTerminalIsUsed;
            set => this.SetProperty(ref this.pOSTerminalIsUsed, value);
        }

        /// <summary>
        /// Gets or sets a properties of a POS-terminal.
        /// </summary>
        /// <date>23.03.2022.</date>
        public DeviceSettingsModel POSTerminal
        {
            get => this.pOSTerminal;
            set
            {
                if (this.pOSTerminal != null)
                {
                    this.pOSTerminal.PropertyChanged -= this.POSTerminal_PropertyChanged;
                }

                this.SetProperty(ref this.pOSTerminal, value);
                this.pOSTerminal.PropertyChanged += this.POSTerminal_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether connection with AxisCloud is activated.
        /// </summary>
        /// <date>23.03.2022.</date>
        public bool AxisCloudIsUsed
        {
            get => this.axisCloudIsUsed;
            set => this.SetProperty(ref this.axisCloudIsUsed, value);
        }

        /// <summary>
        /// Gets or sets a properties of an AxisCloud app.
        /// </summary>
        /// <date>23.03.2022.</date>
        public DeviceSettingsModel AxisCloud
        {
            get => this.axisCloud;
            set => this.SetProperty(ref this.axisCloud, value);
        }

        /// <summary>
        /// Gets or sets license code.
        /// </summary>
        /// <date>23.03.2022.</date>
        public string LicenseCode
        {
            get => this.licenseCode;
            set => this.SetProperty(ref this.licenseCode, value);
        }

        /// <summary>
        /// Sets values to fields of a company data.
        /// </summary>
        /// <date>23.03.2022.</date>
        private void SetCompanyData()
        {
            this.Company = new CompanyModel();
            this.Company.Name = this.settings.AppSettings[Enums.ESettingKeys.Company];
            this.Company.Principal = this.settings.AppSettings[Enums.ESettingKeys.Principal];
            this.Company.City = this.settings.AppSettings[Enums.ESettingKeys.City];
            this.Company.Address = this.settings.AppSettings[Enums.ESettingKeys.Address];
            this.Company.Phone = this.settings.AppSettings[Enums.ESettingKeys.Phone];
            this.Company.TaxNumber = this.settings.AppSettings[Enums.ESettingKeys.TaxNumber];
            this.Company.VATNumber = this.settings.AppSettings[Enums.ESettingKeys.VATNumber];
            this.Company.BankName = this.settings.AppSettings[Enums.ESettingKeys.BankName];
            this.Company.BankBIC = this.settings.AppSettings[Enums.ESettingKeys.BankBIC];
            this.Company.IBAN = this.settings.AppSettings[Enums.ESettingKeys.IBAN];
            this.Company.OnlineShopNumber = this.settings.AppSettings[Enums.ESettingKeys.OnlineShopNumber];
            this.Company.OnlineShopDomainName = this.settings.AppSettings[Enums.ESettingKeys.OnlineShopDomainName];

            this.OnlineShopTypes = new ObservableCollection<ComboBoxItemModel>();
            foreach (EOnlineShopTypes shopType in Enum.GetValues(typeof(EOnlineShopTypes)))
            {
                this.OnlineShopTypes.Add(new ComboBoxItemModel()
                {
                    Text = shopType.ToString(),
                    Value = shopType,
                });

                if ((int)this.settings.AppSettings[Enums.ESettingKeys.OnlineShopType] == (int)shopType)
                {
                    this.Company.ShopType = this.OnlineShopTypes[this.OnlineShopTypes.Count - 1];
                }
            }

            if (!string.IsNullOrEmpty(this.settings.LogoPath))
            {
                using (Stream reader = new FileStream(this.settings.LogoPath, FileMode.Open, FileAccess.Read))
                {
                    this.CompanyLogo = Image.FromStream(reader);
                }
            }
        }

        /// <summary>
        /// Sets values to fields of an properties of the applacation.
        /// </summary>
        /// <date>23.03.2022.</date>
        private void SetApplicationProperties()
        {
            this.USN = ((int)this.settings.AppSettings[Enums.ESettingKeys.UniqueSaleNumber]).ToString("D7");
            this.OperatorCode = (int)this.settings.FiscalPrinterSettings[Enums.ESettingKeys.OperatorCode];
            this.OperatorName = this.settings.AppSettings[Enums.ESettingKeys.UserName];

            this.Languages = new ObservableCollection<ComboBoxItemModel>();
            foreach (ELanguages language in this.settings.SupportedLanguages)
            {
                this.Languages.Add(new ComboBoxItemModel()
                {
                    Text = language.ToString(),
                    Value = language,
                });

                if (language == this.settings.AppLanguage)
                {
                    this.SelectedLanguage = this.Languages[this.Languages.Count - 1];
                }
            }
        }

        /// <summary>
        /// Sets values to fields of properties of a scanner.
        /// </summary>
        /// <date>23.03.2022.</date>
        private void SetScannerProperties()
        {
            this.COMPorts = new ObservableCollection<string>();
            this.COMPorts.Add("Disabled");
            foreach (string port in DeviceHelper.GetSerialPorts())
            {
                this.COMPorts.Add(port);

                if (port.Equals(this.settings.COMScannerSettings[Enums.ESettingKeys.ComPort]))
                {
                    this.SelectedScannerCOMPort = port;
                }
            }
        }

        /// <summary>
        /// Sets values to fields of properties of a fiscal printer and POS-terminal.
        /// </summary>
        /// <date>23.03.2022.</date>
        private void SetDeviceProperties()
        {
            this.FiscalPrinterIsUsed = (bool)this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceIsUsed];
            this.FiscalPrinter = new DeviceSettingsModel();
            this.FiscalPrinterManufacturers = new ObservableCollection<ComboBoxItemModel>();
            this.FiscalPrinterModels = new ObservableCollection<ComboBoxItemModel>();
            this.FiscalPrinterProtocols = new ObservableCollection<ComboBoxItemModel>();
            foreach (IPrinterManufacturer manufacturer in FiscalPrinterModel.GetFiscalPrinterManufacturers())
            {
                this.FiscalPrinterManufacturers.Add(new ComboBoxItemModel()
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.Manufacturer,
                });

                if (manufacturer.Name == this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceManufacturer])
                {
                    this.FiscalPrinter.Manufacturer = this.FiscalPrinterManufacturers[this.FiscalPrinterManufacturers.Count - 1];
                }
            }

            this.BaudRates = new ObservableCollection<int>();
            foreach (int speed in DeviceHelper.GetDefaulfBaudRates())
            {
                this.BaudRates.Add(speed);
            }

            this.POSTerminalIsUsed = (bool)this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceIsUsed];
            this.POSTerminalManufacturers = new ObservableCollection<ComboBoxItemModel>();
            this.POSTerminalModels = new ObservableCollection<ComboBoxItemModel>();
            this.POSTerminalProtocols = new ObservableCollection<ComboBoxItemModel>();
            foreach (IPinPadManufacturer manufacturer in POSTerminalModel.GetPOSTerminalManufacturers())
            {
                this.POSTerminalManufacturers.Add(new ComboBoxItemModel()
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.Manufacturer,
                });

                if (manufacturer.Name == this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceManufacturer])
                {
                    this.POSTerminal.Manufacturer = this.POSTerminalManufacturers[this.POSTerminalManufacturers.Count - 1];
                }
            }
        }

        /// <summary>
        /// Sets values to fields of properties of a fiscal printer.
        /// </summary>
        /// <date>23.03.2022.</date>
        private void SetAxisCloudProperties()
        {
            this.AxisCloud = new DeviceSettingsModel();
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                if (socket.LocalEndPoint is IPEndPoint endPoint)
                {
                    this.AxisCloud.IPAddress = endPoint.Address.ToString();
                }
            }
        }

        /// <summary>
        /// Sets data to dependents properties if main property was changed.
        /// </summary>
        /// <param name="sender">DeviceSettingsModel of a fiscal printer.</param>
        /// <param name="e">Event args.</param>
        /// <date>23.03.2022.</date>
        private void FiscalPrinter_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            IPrinterManufacturer selectedManufacturer;
            IDefaultPrinterConfiguration selectedPrinter;

            switch (e.PropertyName)
            {
                case nameof(DeviceSettingsModel.Manufacturer):
                    this.FiscalPrinterModels.Clear();

                    List<IDefaultPrinterConfiguration> printersList = FiscalPrinterModel.GetFiscalPrinterModels(this.FiscalPrinter.Manufacturer.Text);
                    foreach (IDefaultPrinterConfiguration printer in printersList)
                    {
                        this.FiscalPrinterModels.Add(new ComboBoxItemModel()
                        {
                            Text = printer.Name,
                            Value = printer.PrinterType,
                        });

                        if (printer.PrinterType.ToString().Equals(this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceModel]))
                        {
                            this.FiscalPrinter.Model = this.FiscalPrinterModels[this.FiscalPrinterModels.Count - 1];
                        }
                    }

                    break;
                case nameof(DeviceSettingsModel.Model):
                    this.FiscalPrinterProtocols.Clear();

                    selectedManufacturer = FiscalPrinterModel.ParseToPrinterManufacturer(this.FiscalPrinter.Manufacturer.Value.ToString());
                    selectedPrinter = FiscalPrinterModel.ParseToPrinterModel(selectedManufacturer, this.FiscalPrinter.Model.Value.ToString());
                    List<IConfiguration> protocolsList = FiscalPrinterModel.GetSupportedProtocols(selectedPrinter);
                    foreach (IConfiguration protocol in protocolsList)
                    {
                        this.FiscalPrinterProtocols.Add(new ComboBoxItemModel()
                        {
                            Text = protocol.Name,
                            Value = protocol.Type,
                        });

                        if (protocol.Type.ToString().Equals(this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceProtocol]))
                        {
                            this.FiscalPrinter.Protocol = this.FiscalPrinterProtocols[this.FiscalPrinterProtocols.Count - 1];
                        }
                    }

                    if (this.FiscalPrinter.Protocol == null && this.FiscalPrinterProtocols.Count > 0)
                    {
                        this.FiscalPrinter.Protocol = this.FiscalPrinterProtocols[0];
                    }

                    this.FiscalPrinter.Login =
                        string.IsNullOrEmpty(this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceLogin]) ?
                        selectedPrinter.UserCode :
                        this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceLogin];
                    this.FiscalPrinter.Password =
                        string.IsNullOrEmpty(this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DevicePassword]) ?
                        selectedPrinter.UserPassword :
                        this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DevicePassword];

                    break;
                case nameof(DeviceSettingsModel.Protocol):
                    selectedManufacturer = FiscalPrinterModel.ParseToPrinterManufacturer(this.FiscalPrinter.Manufacturer.Value.ToString());
                    selectedPrinter = FiscalPrinterModel.ParseToPrinterModel(selectedManufacturer, this.FiscalPrinter.Model.Value.ToString());

                    switch ((SupportedCommunicationEnum)this.FiscalPrinter.Protocol.Value)
                    {
                        case SupportedCommunicationEnum.Serial:
                            this.FiscalPrinter.BaudRate = FiscalPrinterModel.GetDefaultBaudRate(selectedPrinter);

                            break;
                        case SupportedCommunicationEnum.Lan:
                            this.FiscalPrinter.IPAddress = this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceIPAddress];
                            this.FiscalPrinter.IPPort =
                                string.IsNullOrEmpty(this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceIPPort]) ?
                                FiscalPrinterModel.GetDefaultIPPort(selectedPrinter) :
                                (int)this.settings.FiscalPrinterSettings[Enums.ESettingKeys.DeviceIPPort];

                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// Sets data to dependents properties if main property was changed.
        /// </summary>
        /// <param name="sender">DeviceSettingsModel of a POS-terminal.</param>
        /// <param name="e">Event args.</param>
        /// <date>24.03.2022.</date>
        private void POSTerminal_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DeviceSettingsModel.Manufacturer):
                    this.POSTerminalModels.Clear();

                    List<IDefaultPinPadConfiguration> terminalModels = POSTerminalModel.GetPOSTerminalModels(this.POSTerminal.Manufacturer.Value.ToString());
                    foreach (IDefaultPinPadConfiguration terminalModel in terminalModels)
                    {
                        this.POSTerminalModels.Add(new ComboBoxItemModel()
                        {
                            Text = terminalModel.Name.ToString(),
                            Value = terminalModel.PinPadType,
                        });

                        if (this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceModel].Equals(terminalModel.PinPadType.ToString()))
                        {
                            this.POSTerminal.Model = this.POSTerminalModels[this.POSTerminalModels.Count - 1];
                        }
                    }

                    break;
                case nameof(DeviceSettingsModel.Model):
                    this.POSTerminalProtocols.Clear();

                    List<IConfiguration> protocolsList = POSTerminalModel.GetSupportedProtocols(
                        this.POSTerminal.Manufacturer.Value.ToString(),
                        this.POSTerminal.Model.Value.ToString());
                    foreach (IConfiguration protocol in protocolsList)
                    {
                        this.POSTerminalProtocols.Add(new ComboBoxItemModel()
                        {
                            Text = protocol.Name,
                            Value = protocol.Type,
                        });

                        if (this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceProtocol].Equals(protocol.Type.ToString()))
                        {
                            this.POSTerminal.Protocol = this.POSTerminalProtocols[this.POSTerminalProtocols.Count - 1];
                        }
                    }

                    if (this.POSTerminal.Protocol == null && this.POSTerminalProtocols.Count > 0)
                    {
                        this.POSTerminal.Protocol = this.POSTerminalProtocols[0];
                    }

                    break;
                case nameof(DeviceSettingsModel.Protocol):
                    IPinPadManufacturer selectedManufacturer = POSTerminalModel.ParseToPOSTerminalManufacturer(this.POSTerminal.Manufacturer.Value.ToString());
                    IDefaultPinPadConfiguration selectedModel = POSTerminalModel.ParseToPOSTerminalModel(
                        selectedManufacturer,
                        this.POSTerminal.Model.Value.ToString());
                    switch ((SupportedCommunicationEnum)this.POSTerminal.Protocol.Value)
                    {
                        case SupportedCommunicationEnum.Serial:
                            this.POSTerminal.BaudRate = POSTerminalModel.GetDefaultBaudRate(selectedModel);
                            break;
                        case SupportedCommunicationEnum.Lan:
                            this.POSTerminal.IPAddress = this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceIPAddress];
                            this.POSTerminal.IPPort =
                                string.IsNullOrEmpty(this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceIPPort]) ?
                                POSTerminalModel.GetDefaultIPPort(selectedModel) :
                                (int)this.settings.POSTerminalSettings[Enums.ESettingKeys.DeviceIPPort];
                            break;
                    }

                    break;
            }
        }

        [ICommand]
        private void ChangeCompanyLogo()
        {

        }

        [ICommand]
        private void ExportLogFile()
        {

        }

        [ICommand]
        private void SaveChanges()
        {

        }

        [ICommand]
        private void ActivateLicense()
        {

        }

        [ICommand]
        private void BuyLicense()
        {

        }
    }
}
