// <copyright file="SerializationModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.AutofacModules
{
    using Autofac;
    using AxisUno.Services.Serialization;

    /// <summary>
    /// Register serialization service.
    /// </summary>
    public sealed class SerializationModule : Module
    {
        /// <summary>
        /// Add registration to container.
        /// </summary>
        /// <param name="builder">Container in which service will be added.</param>
        /// <date>16.03.2022.</date>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SerializationService>().As<ISerializationService>().InstancePerDependency();
        }
    }
}
