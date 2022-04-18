// <copyright file="TranslationModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.AutofacModules
{
    using Autofac;
    using AxisUno.Services.Translation;

    /// <summary>
    /// Register translation service.
    /// </summary>
    public sealed class TranslationModule : Module
    {
        /// <summary>
        /// Add registration to container.
        /// </summary>
        /// <param name="builder">Container in which service will be added.</param>
        /// <date>12.04.2022.</date>
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TranslationService>().As<ITranslationService>().InstancePerLifetimeScope();
            //builder.RegisterType<TranslationService>().As<ITranslationService>().SingleInstance();
        }
    }
}
