package br.com.octoenigma.address.infrastructure.di;

import com.google.inject.AbstractModule;

import br.com.octoenigma.address.domain.AddressRepository;
import br.com.octoenigma.address.infrastructure.remotes.RemoteZipCodeServiceProvider;
import br.com.octoenigma.address.infrastructure.repositories.InMemoryAddressRepository;
import br.com.octoenigma.address.infrastructure.settings.Settings;
import br.com.octoenigma.address.infrastructure.settings.SettingsProvider;
import br.com.octoenigma.address.services.AddressService;
import br.com.octoenigma.address.services.ZipCodeService;
import br.com.octoenigma.address.services.impl.AddressServiceImpl;

public class DependencyManager extends AbstractModule {

	@Override
	protected void configure() {
		// repositories
		bind(AddressRepository.class).to(InMemoryAddressRepository.class);

		// services
		bind(AddressService.class).to(AddressServiceImpl.class);

		// providers
		bind(Settings.class).toProvider(SettingsProvider.class);
		bind(ZipCodeService.class).toProvider(RemoteZipCodeServiceProvider.class);
	}

}