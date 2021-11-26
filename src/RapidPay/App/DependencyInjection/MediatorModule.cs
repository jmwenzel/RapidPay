using Autofac;
using MediatR;
using RapidPay.App.Cqs.Commands.Card;
using RapidPay.App.Cqs.Commands.Payment;
using RapidPay.App.Cqs.Queries.Card;
using System.Reflection;

namespace RapidPay.App.DependencyInjection
{
    /// <summary>
    /// Mediator module with Autofac
    /// </summary>
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register mediator
            builder.RegisterAssemblyTypes(typeof(IMediator)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateCardAsyncCmd).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreatePaymentAsyncCmd).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(GetCardAsyncQuery).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
