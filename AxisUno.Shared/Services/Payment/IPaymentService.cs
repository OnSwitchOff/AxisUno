// <copyright file="IPaymentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Payment
{
    /// <summary>
    /// Describes payment modules.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Gets the class to make a payment.
        /// </summary>
        /// <date>15/03/2022.</date>
        IDevice FiscalDevice { get; }

        /// <summary>
        /// Gets number of receipt.
        /// </summary>
        /// <param name="fiscalDevice">Class to make a payment.</param>
        /// <date>15/03/2022.</date>
        void SetPaymentTool(IDevice fiscalDevice);
    }
}
