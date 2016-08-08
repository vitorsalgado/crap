package br.com.octoenigma.address.services.dtos;

import br.com.octoenigma.address.domain.Address;

public final class AddressDtoFactory {
	public static AddressDto toDto(Address address) {
		AddressDto dto = new AddressDto();

		dto.setCity(address.getCity());
		dto.setDetail(address.getDetail());
		dto.setId(address.getId());
		dto.setNeighborhood(address.getNeighborhood());
		dto.setNumber(address.getNumber());
		dto.setReference(address.getReference());
		dto.setState(address.getState());
		dto.setStreet(address.getStreet());
		dto.setType(address.getType());
		dto.setZipCode(address.getZipCode());

		return dto;
	}
}
