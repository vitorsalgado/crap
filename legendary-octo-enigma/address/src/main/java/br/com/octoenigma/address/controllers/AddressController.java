package br.com.octoenigma.address.controllers;

import static spark.Spark.before;
import static spark.Spark.delete;
import static spark.Spark.get;
import static spark.Spark.post;
import static spark.Spark.put;

import java.util.Optional;

import javax.inject.Inject;

import org.eclipse.jetty.http.HttpStatus;

import br.com.ocotoenigma.commons.messages.Response;
import br.com.ocotoenigma.commons.rest.RestController;
import br.com.octoenigma.address.services.AddressService;
import br.com.octoenigma.address.services.dtos.AddressDto;

public class AddressController extends RestController {

	private final AddressService addressService;

	@Inject
	public AddressController(AddressService addressService) {
		this.addressService = addressService;
	}

	@Override
	public void buildRoutes() {
		before("/api/cep", APPLICATION_JSON, (req, res) -> res.type(APPLICATION_JSON));

		get("/api/cep/:id", (req, res) -> {
			Optional<Long> id = parseId(req.params(":id"));

			if (id.isPresent()) {
				Response<AddressDto> response = addressService.findById(id.get());

				if (response.getData() == null) {
					res.status(HttpStatus.NOT_FOUND_404);
				}

				return response;
			}

			res.status(HttpStatus.BAD_REQUEST_400);

			return new Response.Builder<Void>()
					.asFail()
					.withMsg("Formato de identificador de endereço inválido")
					.build();

		}, this::toJson);

		post("/api/cep", (req, res) -> {
			AddressDto dto = (AddressDto) this.fromJson(AddressDto.class, req.body());
			Response<AddressDto> response = addressService.save(dto);

			if (response.isSuccess()) {
				res.status(HttpStatus.CREATED_201);

				return response;
			}

			res.status(HttpStatus.BAD_REQUEST_400);

			return response;
		}, this::toJson);

		put("/api/cep", (req, res) -> {
			AddressDto dto = (AddressDto) this.fromJson(AddressDto.class, req.body());

			if (dto.getId() <= 0) {
				res.status(HttpStatus.BAD_REQUEST_400);

				return new Response.Builder<Void>()
						.asFail()
						.withMsg("Identificador de endereço inválido")
						.build();
			}

			Response<AddressDto> response = addressService.save(dto);

			if (response.isSuccess()) {
				res.status(HttpStatus.OK_200);

				return response;
			}

			res.status(HttpStatus.BAD_REQUEST_400);

			return response;
		}, this::toJson);

		delete("/api/cep/:id", (req, res) -> {
			Optional<Long> id = parseId(req.params(":id"));

			if (id.isPresent()) {
				return addressService.delete(id.get());
			}

			res.status(HttpStatus.BAD_REQUEST_400);

			return new Response.Builder<Void>()
					.asFail()
					.withMsg("Formato de identificador de endereço inválido")
					.build();

		}, this::toJson);
	}

	private static Optional<Long> parseId(String incomingParam) {
		try {
			return Optional.of(Long.parseLong(incomingParam));
		} catch (NumberFormatException ex) {
			return Optional.empty();
		}
	}

}
