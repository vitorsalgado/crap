package br.com.octoenigma.zipcode;

import static spark.Spark.port;

import com.google.inject.Guice;
import com.google.inject.Injector;

import br.com.ocotoenigma.commons.rest.ErrorController;
import br.com.octoenigma.zipcode.controllers.ZipCodeController;

public class Application {
	public static void main(String[] args) {
		Injector injector = Guice.createInjector(new DependencyManager());

		port(7070);

		initRoutes(injector);
	}

	static void initRoutes(Injector injector) {
		injector.getInstance(ErrorController.class).buildRoutes();
		injector.getInstance(ZipCodeController.class).buildRoutes();
	}
}
