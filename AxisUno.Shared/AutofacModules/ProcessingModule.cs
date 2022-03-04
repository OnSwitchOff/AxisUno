using Autofac;
using AxisUno.Infrastructure.Domain.UnitOfWorks;
using AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;
using AxisUno.PipeLines.Validation;
using AxisUNO.Decorators;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.AutofacModules
{
    public sealed class ProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>().As<IDomainEventsDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ValidationRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));

            builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<,>), typeof(IRequestHandler<,>));

            builder.RegisterGenericDecorator(typeof(DiagnosticQueryHandlerDecorator<,>), typeof(IRequestHandler<,>));

            builder.RegisterGenericDecorator(typeof(LoggingRequestHandlerDecorator<,>), typeof(IRequestHandler<,>));
        }
    }
}
