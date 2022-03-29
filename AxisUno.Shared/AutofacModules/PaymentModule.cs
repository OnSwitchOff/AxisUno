// <copyright file="PaymentModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.AutofacModules
{
    using Autofac;
    using AxisUno.Services.Payment;

    /// <summary>
    /// Register settings service.
    /// </summary>
    public sealed class PaymentModule : Module
    {
        /// <summary>
        /// Add registration to container.
        /// </summary>
        /// <param name="builder">Container in which service will be added.</param>
        /// <date>17.03.2022.</date>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentService>().As<IPaymentService>().SingleInstance();
        }
    }
}
