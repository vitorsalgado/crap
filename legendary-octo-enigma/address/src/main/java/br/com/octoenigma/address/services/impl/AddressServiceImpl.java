package br.com.octoenigma.address.services.impl;

import java.util.Optional;

import javax.inject.Inject;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.address.domain.Address;
import br.com.octoenigma.address.domain.AddressRepository;
import br.com.octoenigma.address.services.AddressService;
import br.com.octoenigma.address.services.ZipCodeService;
import br.com.octoenigma.address.services.dtos.AddressDto;
import br.com.octoenigma.address.services.dtos.AddressDtoFactory;
import br.com.octoenigma.address.services.dtos.ZipCodeDto;

public class AddressServiceImpl implements AddressService {

	private final ZipCodeService zipCodeService;
	private final AddressRepository addressRepository;

	@Inject
	public AddressServiceImpl(ZipCodeService zipCodeService, AddressRepository addressRepository) {
		this.zipCodeService = zipCodeService;
		this.addressRepository = addressRepository;
	}

	@Override
	public Response<AddressDto> save(AddressDto dto) {
		Response<ZipCodeDto> response = zipCodeService.findZipCodeAddress(dto.getZipCode());

		if (!response.isSuccess()) {
			return new Response.Builder<AddressDto>()
					.asFail()
					.withMsg(response.getMessage())
					.withDetails(response.getDetails()).build();
		}

		Address address = new Address(dto.getZipCode(), dto.getType(), dto.getStreet(), dto.getNumber(),
				dto.getNeighborhood(), dto.getCity(), dto.getState());

		address.setDetail(dto.getDetail());
		address.setReference(dto.getReference());

		if (dto.getId() > 0) {
			address.setId(dto.getId());
		}

		if (address.validate()) {
			addressRepository.save(address);

			return new Response.Builder<AddressDto>().asSuccess().withData(AddressDtoFactory.toDto(address)).build();
		}

		return new Response.Builder<AddressDto>().asFail().withMsg("Existem campos inválidos no endereço")
				.withValidationErrors(address.getValidationErrors()).build();
	}

	@Override
	public Response<AddressDto> findById(long id) {
		if (id <= 0) {
			return new Response.Builder<AddressDto>().asFail()
					.withMsg("identificador de endereço inválido. O valor deve ser maior do que zero.").build();
		}

		Optional<Address> foundAddress = addressRepository.findById(id);

		if (foundAddress.isPresent()) {
			return new Response.Builder<AddressDto>().asSuccess().withData(AddressDtoFactory.toDto(foundAddress.get()))
					.build();
		}

		return new Response.Builder<AddressDto>().asFail()
				.withMsg(String.format("Não foi encontrado nenhum endereço com o identificador %s", id)).build();
	}

	@Override
	public Response<Void> delete(long id) {
		if (id <= 0) {
			return new Response.Builder<Void>()
					.withMsg("identificador de endereço inválido. O valor deve ser maior do que zero.").build();
		}

		Optional<Address> foundAddress = addressRepository.findById(id);

		if (foundAddress.isPresent()) {
			addressRepository.delete(foundAddress.get());

			return new Response.Builder<Void>().build();
		}

		return new Response.Builder<Void>().asFail()
				.withMsg(String.format("Não foi encontrado nenhum endereço com o identificador %s", id)).build();
	}

}
