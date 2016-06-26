using Autofac;
using Autofac.Integration.WebApi;
using PogStore.Cms.AdminApi.CommandResolver;
using PogStore.Cms.ApplicationServices.Users;
using PogStore.Cms.ApplicationServices.Users.Implementations;
using PogStore.Cms.Core.Cqrs.Command;
using PogStore.Cms.Core.Cqrs.Storage;
using PogStore.Cms.Core.Infrastructure.Cache;
using PogStore.Cms.Infrastructure.Cache;
using PogStore.EventStore;
using PogStore.Infrascture.Encryption;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace PogStore.Cms.AdminApi.Infrastructure.DI
{
	public static class DependencyResolverFactory
	{
		public static System.Web.Http.Dependencies.IDependencyResolver BuildForWebApi2()
		{
			return new AutofacWebApiDependencyResolver(BuildAutofacContainer());
		}

		private static IContainer BuildAutofacContainer()
		{
			var builder = new ContainerBuilder();
			var assemblies = GetAssemblies();

			builder.RegisterApiControllers(assemblies);
			builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

			RegisterCoreComponents(builder);
			RegisterCommandHandlers(builder);
			RegisterApplicationServices(builder);

			return builder.Build();
		}

		private static void RegisterApplicationServices(ContainerBuilder builder)
		{
			builder.RegisterType<UserServiceImpl>().As<IUserService>().InstancePerRequest();
		}

		private static void RegisterCommandHandlers(ContainerBuilder builder)
		{
			//builder.RegisterType<CreateUserHandler>().As<ICommandHandler<CreateUserCommand>>().InstancePerRequest();
		}

		private static void RegisterCoreComponents(ContainerBuilder builder)
		{
			builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerRequest();
			builder.RegisterType<WebApiCommandResolver>().As<ICommandHandlerResolver>().InstancePerRequest();
			builder.RegisterType<JsonEventSerializer>().As<IEventSerializer>().InstancePerRequest();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
			builder.RegisterType<SqlServerEventStore>().As<IEventStore>().InstancePerRequest();
			builder.RegisterType<DomainRepository>().As<IDomainRepository>().InstancePerRequest();

			builder.RegisterType<TripleDESEncryptionService>().As<IEncryptionService>().InstancePerRequest();
			builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerRequest();
		}

		private static Assembly[] GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.Contains("PogStore"))
				.ToArray();
		}
	}
}