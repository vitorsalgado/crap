using System;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using CrimaMap.ApiClient.Users;
using CrimeMap.WebUI.Infrastructure.Authentication;
using CrimeMap.WebUI.Infrastructure.Configuration;
using CrimeMap.WebUI.Infrastructure.Context;

namespace CrimeMap.WebUI.Infrastrucure.DI {

	public static class DependencyResolverFactory {

		public static System.Web.Mvc.IDependencyResolver BuildForMvc() {
			return new AutofacDependencyResolver(BuildAutofacContainer());
		}

		private static IContainer BuildAutofacContainer() {
			var builder = new ContainerBuilder();

			RegisterTypes(builder);

			IContainer container = builder.Build();

			return container;
		}

		private static void RegisterTypes(ContainerBuilder builder) {
			var assemblies = GetAssemblies();

			builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase).As<HttpContextBase>().InstancePerRequest();

			builder.RegisterControllers(assemblies);
			builder.RegisterFilterProvider();

			builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerRequest();
			builder.RegisterType<ConfigurationService>().As<IConfigurationService>().SingleInstance();
			builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerRequest();

			builder.RegisterType<UserProxy>().As<IUserProxy>().InstancePerRequest();
		}

		private static Assembly[] GetAssemblies() {
			return AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.Contains("CrimeMap.WebUI"))
				.ToArray();
		}

	}
}
