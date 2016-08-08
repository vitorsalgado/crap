package br.com.octoenigma.address;

import static spark.Spark.port;

import com.google.inject.Guice;
import com.google.inject.Injector;

import br.com.ocotoenigma.commons.rest.ErrorController;
import br.com.octoenigma.address.controllers.AddressController;
import br.com.octoenigma.address.infrastructure.di.DependencyManager;
import br.com.octoenigma.address.infrastructure.settings.Settings;

public class Application {
	public static void main(String[] args) {
		Injector injector = Guice.createInjector(new DependencyManager());
		Settings settings = injector.getInstance(Settings.class);

		port(settings.getPort());

		initRoutes(injector);
	}

	static void initRoutes(Injector injector) {
		injector.getInstance(ErrorController.class).buildRoutes();
		injector.getInstance(AddressController.class).buildRoutes();
	}

}
