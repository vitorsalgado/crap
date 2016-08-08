package br.com.octoenigma.zipcode.models;

import java.util.Optional;

public interface ZipCodeRepository {
	Optional<ZipCode> findByCode(String code);
}
