package br.com.octoenigma.address.test;

import static org.junit.Assert.assertFalse;
import static org.mockito.Matchers.anyLong;
import static org.mockito.Mockito.when;

import java.util.Optional;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.mockito.runners.MockitoJUnitRunner;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.address.domain.AddressRepository;
import br.com.octoenigma.address.services.ZipCodeService;
import br.com.octoenigma.address.services.dtos.AddressDto;
import br.com.octoenigma.address.services.impl.AddressServiceImpl;

@RunWith(MockitoJUnitRunner.class)
public class AddressServiceTest {

	AddressRepository addressRepository;
	ZipCodeService zipCodeService;
	
	AddressServiceImpl addressServiceImpl;

	@Before
	public void setUp() {
		addressRepository = Mockito.mock(AddressRepository.class);
		zipCodeService = Mockito.mock(ZipCodeService.class);
		
		addressServiceImpl = new AddressServiceImpl(zipCodeService, addressRepository);
	}

	@Test
	public void find_address_with_innexistent_id_should_return_failure() {
		when(addressRepository.findById(anyLong())).thenReturn(Optional.empty());

		Response<AddressDto> response = addressServiceImpl.findById(55000);

		assertFalse(response.isSuccess());
		assertFalse(response.getMessage().isEmpty());
	}

	@Test
	public void find_address_with_negative_id_should_return_failure() {
		long id = -100;
		when(addressRepository.findById(id)).thenReturn(Optional.empty());

		Response<AddressDto> response = addressServiceImpl.findById(id);

		assertFalse(response.isSuccess());
		assertFalse(response.getMessage().isEmpty());
	}

}
