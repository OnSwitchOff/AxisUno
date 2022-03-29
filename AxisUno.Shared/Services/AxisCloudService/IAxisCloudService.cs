// <copyright file="IAxisCloudService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.AxisCloudService
{
    using System;
    using AxisUno.Services.Settings;
    using Microinvest.IntegrationService.Enums.AxisCloud;

    /// <summary>
    /// Delegate describes structure of event that signal a change in the status of the service.
    /// </summary>
    /// <param name="status">Status of work with the service.</param>
    /// <param name="errorException">Exception thrown during service work.</param>
    /// <date>22.03.2022.</date>
    public delegate void ServiceStatusDelegate(EServiceStatus status, Exception errorException);

    /// <summary>
    /// Describes service to communicate with AxisCloud app.
    /// </summary>
    public interface IAxisCloudService
    {
        /// <summary>
        /// Event that signal a change in the status of work with the service.
        /// </summary>
        /// <date>22.03.2022.</date>
        event ServiceStatusDelegate StatusChanged;

        /// <summary>
        /// Gets status of work with the service.
        /// </summary>
        /// <date>22.03.2022.</date>
        EServiceStatus ServiceStatus { get; }

        /// <summary>
        /// Start listening a port.
        /// </summary>
        /// <param name="port">Port to listen.</param>
        /// <param name="settingsService">Settings of the application.</param>
        /// <date>22.03.2022.</date>
        void StartServiceAsync(int port, ISettingsService settingsService);

        /// <summary>
        /// Stop listening a port.
        /// </summary>
        /// <date>22.03.2022.</date>
        void StopService();
    }
}
