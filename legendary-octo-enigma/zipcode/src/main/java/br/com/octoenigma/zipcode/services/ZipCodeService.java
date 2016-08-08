package br.com.octoenigma.zipcode.services;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.zipcode.models.ZipCode;

public interface ZipCodeService {
	Response<ZipCode> findByCode(String code);
}
