package br.com.octoenigma.address.services;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.address.services.dtos.ZipCodeDto;
import feign.Param;
import feign.RequestLine;

public interface ZipCodeService {
	@RequestLine("GET /api/cep/{code}")
	Response<ZipCodeDto> findZipCodeAddress(@Param("code") String code);
}
