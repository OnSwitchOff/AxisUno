// <copyright file="PaymentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Payment
{
    using System;
    using HarabaSourceGenerators.Common.Attributes;

    /// <summary>
    /// Describes payment service.
    /// </summary>
    // [Inject]
    public partial class PaymentService : IPaymentService
    {
        private IDevice? fiscalDevice = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        public PaymentService()
        {
            this.fiscalDevice = null;
        }

        /// <summary>
        /// Gets the class to make a payment.
        /// </summary>
        /// <date>15/03/2022.</date>
        public IDevice FiscalDevice
        {
            get
            {
                if (this.fiscalDevice == null)
                {
                    throw new Exception("Device doesn't initialized!");
                }

                return this.fiscalDevice;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the class to make a payment is initialized.
        /// </summary>
        /// <date>19/04/2022.</date>
        public bool FiscalDeviceInitialized => this.fiscalDevice != null;

        /// <summary>
        /// Gets number of receipt.
        /// </summary>
        /// <param name="fiscalDevice">Class to make a payment.</param>
        /// <date>15/03/2022.</date>
        public void SetPaymentTool(IDevice fiscalDevice)
        {
            this.fiscalDevice = fiscalDevice;
        }
    }
}
