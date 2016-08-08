package br.com.octoenigma.address.infrastructure.remotes;

import javax.inject.Inject;
import javax.inject.Provider;

import br.com.octoenigma.address.infrastructure.settings.Settings;
import br.com.octoenigma.address.services.ZipCodeService;
import feign.Feign;
import feign.gson.GsonDecoder;
import feign.gson.GsonEncoder;

public class RemoteZipCodeServiceProvider implements Provider<ZipCodeService> {

	private final Settings settings;
	
	@Inject
	public RemoteZipCodeServiceProvider(Settings settings) {
		this.settings = settings;
	}
	
	@Override
	public ZipCodeService get() {
		return Feign.builder()
				.encoder(new GsonEncoder())
				.decoder(new GsonDecoder())
				.target(ZipCodeService.class, settings.getZipCodeServiceEndpoint());
	}

}
