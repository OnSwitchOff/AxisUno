using Autofac;
using AxisUno.AutofacModules.Helpers;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace AxisUno.AutofacModules
{
    public sealed class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatR(MediatorAssemblies.ApplicationLayer);

            var openHandlerTypes = new[]
            {
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>),
        };

            foreach (var openHandlerType in openHandlerTypes)
            {
                builder.RegisterAssemblyTypes(MediatorAssemblies.ApplicationLayer)
                    .AsClosedTypesOf(openHandlerType)
                    .AsImplementedInterfaces();
            }
        }
    }
}
