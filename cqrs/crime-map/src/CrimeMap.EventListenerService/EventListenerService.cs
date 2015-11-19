using System;
using Autofac;
using CrimeMap.EventHandlers.Users;
using MassTransit;

namespace CrimeMap.EventListenerService {

	public class EventListenerService {

		private IServiceBus _bus;
		private IContainer _container;

		public void Start() {
			var builder = new ContainerBuilder();

			SetUpContainer(builder);
			SetUpMasstransit(builder);

			_container = builder.Build();

			_bus = _container.Resolve<IServiceBus>();
		}

		public void Stop() {
			if (_bus != null) {
				_bus.Dispose();
			}

			if (_container != null) {
				_container.Dispose();
			}
		}

		private static void SetUpContainer(ContainerBuilder builder) {
			builder.RegisterType<UserCreatedHandler>().As<IConsumer>().AsSelf();
		}

		private void SetUpMasstransit(ContainerBuilder builder) {
			builder.Register(c => ServiceBusFactory.New(sbc => {

				sbc.UseRabbitMq(r => {
					r.ConfigureHost(new Uri("rabbitmq://localhost/event_receiver"), h => {
						h.SetUsername("guest");
						h.SetPassword("guest");
					});
				});

				sbc.ReceiveFrom("rabbitmq://localhost/event_receiver");
				sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));

			})).As<IServiceBus>().SingleInstance();
		}

	}

}
