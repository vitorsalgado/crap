using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CrimaMap.ApplicationServices.Users;
using CrimeMap.Api.CommandResolver;
using CrimeMap.Cache;
using CrimeMap.CommandHandlers;
using CrimeMap.CommandHandlers.Users.CommandHandler;
using CrimeMap.Commands.Users;
using CrimeMap.Core.Events;
using CrimeMap.Core.Storage;
using CrimeMap.Encryption;
using CrimeMap.EventHandlers.Infrastructure;
using CrimeMap.EventStore;
using CrimeMap.QueryModel.Users;
using MassTransit;

namespace CrimeMap.Api.Infrastructure.DI {

	public static class DependencyResolverFactory {

		public static System.Web.Http.Dependencies.IDependencyResolver BuildForWebApi2() {
			return new AutofacWebApiDependencyResolver(BuildAutofacContainer());
		}

		private static IContainer BuildAutofacContainer() {
			var builder = new ContainerBuilder();
			var assemblies = GetAssemblies();

			builder.RegisterApiControllers(assemblies);
			builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

			builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerRequest();
			//builder.Register(c => new WebApiCommandResolver() as HttpContextBase).As<HttpContextBase>().InstancePerRequest();
			builder.RegisterType<WebApiCommandResolver>().As<ICommandHandlerResolver>().InstancePerRequest();
			builder.RegisterType<JsonEventSerializer>().As<IEventSerializer>().InstancePerRequest();
			builder.RegisterType<MassTransitEventPublisher>().As<IEventPublisher>().SingleInstance();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
			builder.RegisterType<MySqlEventStore>().As<IEventStore>().InstancePerRequest();
			builder.RegisterType<DomainRepository>().As<IDomainRepository>().InstancePerRequest();
			builder.RegisterType<RegisterUserHandler>().As<ICommandHandler<RegisterUserCommand>>().InstancePerRequest();
			builder.RegisterType<RegisterExternalUserHandler>().As<ICommandHandler<RegisterExternalUserCommand>>().InstancePerRequest();

			builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();
			builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerRequest();

			builder.RegisterType<UserApplicationService>().As<IUserApplicationService>().InstancePerRequest();

			builder.RegisterType<UserQueryService>().As<IUserQueryService>().InstancePerRequest();

			SetUpMassTransit(builder);

			IContainer container = builder.Build();

			return container;
		}

		private static void SetUpMassTransit(ContainerBuilder builder) {
			builder.Register(c => ServiceBusFactory.New(sbc => {

				sbc.UseRabbitMq();
				sbc.ReceiveFrom("rabbitmq://localhost/event_publisher");

			})).As<IServiceBus>().SingleInstance();
		}

		private static Assembly[] GetAssemblies() {
			return AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.Contains("CrimeMap"))
				.ToArray();
		}
	}
}
