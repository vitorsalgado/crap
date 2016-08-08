package br.com.octoenigma.zipcode.repositories;

import java.util.HashMap;
import java.util.Optional;

import br.com.octoenigma.zipcode.models.ZipCode;
import br.com.octoenigma.zipcode.models.ZipCodeRepository;

public class InMemoryZipCodeRepository implements ZipCodeRepository {

	static HashMap<Long, ZipCode> database = new HashMap<>();

	static {
		database.put(1L, new ZipCode(1L, "14555543", "rua teste 1", "bairro 1", "sao paulo", "sp"));
		database.put(2L, new ZipCode(2L, "16575342", "rua teste 2", "bairro 2", "sao paulo", "sp"));
		database.put(3L, new ZipCode(3L, "12546000", "rua teste 3", "bairro 3", "sao paulo", "sp"));
		database.put(4L, new ZipCode(4L, "45678010", "rua teste 4", "bairro 4", "sao paulo", "sp"));
		database.put(5L, new ZipCode(5L, "09789120", "rua teste 5", "bairro 5", "sao paulo", "sp"));
	}

	@Override
	public Optional<ZipCode> findByCode(String code) {
		return database.values().stream().filter(x -> x.getCode().equals(code)).findFirst();
	}

}
