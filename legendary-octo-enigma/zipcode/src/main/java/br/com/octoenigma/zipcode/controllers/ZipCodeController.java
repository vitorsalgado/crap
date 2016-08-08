package br.com.octoenigma.zipcode.controllers;

import static spark.Spark.get;

import com.google.inject.Inject;

import br.com.ocotoenigma.commons.rest.RestController;
import br.com.octoenigma.zipcode.services.ZipCodeService;

public class ZipCodeController extends RestController {

	private final ZipCodeService zipCodeQueryService;

	@Inject
	public ZipCodeController(ZipCodeService zipCodeQueryService) {
		this.zipCodeQueryService = zipCodeQueryService;
	}

	@Override
	public void buildRoutes() {
		get("/api/cep/:code", (req, res) -> {
			res.type(APPLICATION_JSON);
			String incomingZiCode = req.params(":code");

			return zipCodeQueryService.findByCode(incomingZiCode);

		}, this::toJson);
	}

}
