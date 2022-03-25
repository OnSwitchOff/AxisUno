// <copyright file="AxisCloudService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.AxisCloudService
{
    using System;
    using AxisUno.Services.Settings;
    using HarabaSourceGenerators.Common.Attributes;
    using Microinvest.IntegrationService;
    using Microinvest.IntegrationService.Enums.AxisCloud;
    using Microinvest.IntegrationService.Interfaces;

    /// <summary>
    /// Describes service to communicate with AxisCloud app.
    /// </summary>
    [Inject]
    public partial class AxisCloudService : IAxisCloudService
    {
        private AxisCloudIntegrationService integrationService;
        private IAxisCloudIntegration axisCloudHelper;

        /// <summary>
        /// Event that signal a change in the status of work with the service.
        /// </summary>
        /// <date>22.03.2022.</date>
        public event ServiceStatusDelegate StatusChanged;

        /// <summary>
        /// Gets status of work with the service.
        /// </summary>
        /// <date>22.03.2022.</date>
        public EServiceStatus ServiceStatus
        {
            get
            {
                if (this.integrationService == null)
                {
                    throw new Exception("Service to connent to AxisCloud doesn't initialized!");
                }

                return this.integrationService.ServiceStatus;
            }
        }

        /// <summary>
        /// Start listening a port.
        /// </summary>
        /// <param name="port">Port to listen.</param>
        /// <param name="settingsService">Settings of the application.</param>
        /// <date>22.03.2022.</date>
        public void StartServiceAsync(int port, ISettingsService settingsService)
        {
            if (this.integrationService == null)
            {
                this.axisCloudHelper = new AxisCloudHelper(settingsService);
                this.integrationService = new AxisCloudIntegrationService(ref this.axisCloudHelper, port);
            }
            else
            {
                if (this.integrationService.Port != port)
                {
                    if (this.integrationService.ServiceStatus == EServiceStatus.Run)
                    {
                        this.StopService();
                    }

                    this.integrationService.Port = port;
                }
            }

            if (this.integrationService.ServiceStatus == EServiceStatus.Stop)
            {
                this.integrationService.StartListenerAsync(this.StatusChangeHandler);
            }
        }

        /// <summary>
        /// Stop listening a port.
        /// </summary>
        /// <date>22.03.2022.</date>
        public void StopService()
        {
            if (this.integrationService != null)
            {
                this.integrationService.StopListener();
            }
        }

        /// <summary>
        /// Raise an event to signal a change in the status of the service.
        /// </summary>
        /// <param name="status">Service status.</param>
        /// <param name="errorException">Exception thrown during service work.</param>
        /// <date>22.03.2022.</date>
        private void StatusChangeHandler(EServiceStatus status, Exception errorException)
        {
            if (this.StatusChanged != null)
            {
                this.StatusChanged.Invoke(status, errorException);
            }
        }
    }
}
