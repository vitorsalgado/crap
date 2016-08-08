package br.com.octoenigma.address.services;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.address.services.dtos.AddressDto;

public interface AddressService {
	Response<AddressDto> save(AddressDto dto);
	
	Response<AddressDto> findById(long id);
	
	Response<Void> delete(long id);
}
