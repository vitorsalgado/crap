package br.com.octoenigma.zipcode.services.impl;

import java.util.Optional;

import javax.inject.Inject;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.octoenigma.zipcode.models.ZipCode;
import br.com.octoenigma.zipcode.models.ZipCodeRepository;
import br.com.octoenigma.zipcode.services.ZipCodeService;

public class ZipCodeServiceImpl implements ZipCodeService {

	private final ZipCodeRepository zipCodeRepository;

	@Inject
	public ZipCodeServiceImpl(ZipCodeRepository zipCodeRepository) {
		this.zipCodeRepository = zipCodeRepository;
	}

	@Override
	public Response<ZipCode> findByCode(String code) {
		if (code == null || code.isEmpty()) {
			return new Response.Builder<ZipCode>().asFail().withMsg("CEP não pode ser nulo ou vazio").build();
		}

		if (code.equals("00000000")) {
			return new Response.Builder<ZipCode>().asFail()
					.withMsg("Não foi encontrado um endereço para o CEP informado").build();
		}

		Optional<ZipCode> foundCode = zipCodeRepository.findByCode(code);

		if (foundCode.isPresent()) {
			return new Response.Builder<ZipCode>().asSuccess().withData(foundCode.get()).build();
		}

		Boolean canReplace = true;
		StringBuilder sb = new StringBuilder();

		for (int i = code.length() - 1; i >= 0; i--) {
			char currentChar = code.charAt(i);
			char ch = currentChar;

			if (canReplace && currentChar != '0') {
				ch = '0';
				canReplace = false;
			}

			sb.append(ch);
		}

		return findByCode(sb.reverse().toString());
	}

}
