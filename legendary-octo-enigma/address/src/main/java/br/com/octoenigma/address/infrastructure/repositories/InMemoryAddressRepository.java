package br.com.octoenigma.address.infrastructure.repositories;

import java.util.HashMap;
import java.util.Optional;

import br.com.octoenigma.address.domain.Address;
import br.com.octoenigma.address.domain.AddressRepository;

public class InMemoryAddressRepository implements AddressRepository {
	static HashMap<Long, Address> database = new HashMap<>();
	static long sequence = 0;

	@Override
	public Optional<Address> findById(long id) {
		return Optional.ofNullable(database.get(id));
	}

	@Override
	public Optional<Address> findByZipCode(String zipCode) {
		return database.values().stream().filter(x -> x.getZipCode().equals(zipCode)).findFirst();
	}

	@Override
	public void save(Address address) {
		long identifier = address.getId();

		if (identifier == 0) {
			sequence++;
			identifier = sequence;
		}

		database.put(identifier, address);
	}

	@Override
	public void delete(Address address) {
		if (database.containsKey(address.getId())) {
			database.remove(address.getId());
		}
	}
}
