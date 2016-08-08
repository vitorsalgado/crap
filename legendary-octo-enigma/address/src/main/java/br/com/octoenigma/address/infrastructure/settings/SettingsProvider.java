package br.com.octoenigma.address.infrastructure.settings;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

import javax.inject.Provider;

public class SettingsProvider implements Provider<Settings> {

	private static Settings config = null;

	@Override
	public Settings get() {
		return config;
	}

	static {
		InputStream stream = null;
		Properties prop = new Properties();

		try {
			stream = ClassLoader.getSystemResourceAsStream("application.properties");
			prop.load(stream);

			config = new Settings();

			config.setZipCodeServiceEndpoint((prop.getProperty("endpoint.zipcode")));
			config.setPort(Integer.parseInt((prop.getProperty("server.port"))));

		} catch (Exception ex) {
			throw new RuntimeException(ex);
		} finally {
			prop = null;

			if (stream != null) {
				try {
					stream.close();
				} catch (IOException ex) {
					stream = null;
				}
			}
		}
	}
}
