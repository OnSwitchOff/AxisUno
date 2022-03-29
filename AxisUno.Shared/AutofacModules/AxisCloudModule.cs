﻿// <copyright file="AxisCloudModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.AutofacModules
{
    using Autofac;
    using AxisUno.Services.AxisCloudService;

    /// <summary>
    /// Register AxisCloud service.
    /// </summary>
    public sealed class AxisCloudModule : Module
    {
        /// <summary>
        /// Add registration to container.
        /// </summary>
        /// <param name="builder">Container in which service will be added.</param>
        /// <date>22.03.2022.</date>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AxisCloudService>().As<IAxisCloudService>().InstancePerLifetimeScope();
            builder.RegisterType<AxisCloudService>().As<IAxisCloudService>().SingleInstance();
        }
    }
}
