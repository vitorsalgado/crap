package br.com.octoenigma.zipcode;

import com.google.inject.AbstractModule;

import br.com.octoenigma.zipcode.models.ZipCodeRepository;
import br.com.octoenigma.zipcode.repositories.InMemoryZipCodeRepository;
import br.com.octoenigma.zipcode.services.ZipCodeService;
import br.com.octoenigma.zipcode.services.impl.ZipCodeServiceImpl;

public class DependencyManager extends AbstractModule {

	@Override
	protected void configure() {
		// repositories
		bind(ZipCodeRepository.class).to(InMemoryZipCodeRepository.class);
		
		// services
		bind(ZipCodeService.class).to(ZipCodeServiceImpl.class);
	}

}