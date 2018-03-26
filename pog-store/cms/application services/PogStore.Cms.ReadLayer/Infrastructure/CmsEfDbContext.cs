using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace PogStore.Cms.ReadLayer.Infrastructure {

	public class CmsEfDbContext : DbContext {
		public CmsEfDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
		public CmsEfDbContext() : this("pogstore.cms") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => !String.IsNullOrEmpty(type.Namespace))
				.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

			foreach (var type in typesToRegister) {
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
