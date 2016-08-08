package br.com.octoenigma.address.domain;

import java.util.Optional;

public interface AddressRepository {
	Optional<Address> findById(final long id);
	
	Optional<Address> findByZipCode(final String zipCode);
	
	void save(Address address);
	
	void delete(Address address);
}
