package br.com.octoenigma.zipcode;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.mockito.Matchers.anyString;
import static org.mockito.Mockito.when;

import java.util.Optional;

import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.zipcode.models.ZipCode;
import br.com.octoenigma.zipcode.models.ZipCodeRepository;
import br.com.octoenigma.zipcode.services.ZipCodeService;
import br.com.octoenigma.zipcode.services.impl.ZipCodeServiceImpl;

public class ZipCodeSearchTest {

	ZipCodeRepository zipCodeRepository;
	ZipCodeService zipCodeService;

	@Before
	public void setUp() {
		zipCodeRepository = Mockito.mock(ZipCodeRepository.class);
		zipCodeService = new ZipCodeServiceImpl(zipCodeRepository);
	}

	@Test
	public void innexistend_zipcode_must_return_failure() {
		String zipcode = "05416000";

		when(zipCodeRepository.findByCode(anyString())).thenReturn(Optional.empty());

		Response<ZipCode> response = zipCodeService.findByCode(zipcode);

		assertFalse(response.isSuccess());
		assertFalse(response.getMessage().isEmpty());
	}

	@Test
	public void must_retry_search_replacing_last_char_with_zero_until_find_any() {
		String input = "12345678";
		String validOne = "12000000";

		ZipCode model = new ZipCode(1L, validOne, "rua teste", "bairro teste", "cidade", "sp");

		when(zipCodeRepository.findByCode(anyString())).thenReturn(Optional.empty());
		when(zipCodeRepository.findByCode(validOne)).thenReturn(Optional.of(model));

		Response<ZipCode> response = zipCodeService.findByCode(input);

		assertTrue(response.isSuccess());
		assertNotNull(response.getData());
		assertEquals(validOne, response.getData().getCode());
	}

}
