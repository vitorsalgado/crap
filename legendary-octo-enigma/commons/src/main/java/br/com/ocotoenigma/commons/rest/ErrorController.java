package br.com.ocotoenigma.commons.rest;

import static spark.Spark.exception;

import org.apache.commons.lang3.exception.ExceptionUtils;
import org.eclipse.jetty.http.HttpStatus;

import br.com.ocotoenigma.commons.domain.exceptions.DomainException;
import br.com.ocotoenigma.commons.messages.Response.Builder;
import spark.Request;
import spark.Response;

public class ErrorController extends RestController {

	@Override
	public void buildRoutes() {
		exception(DomainException.class, this::handle400);
		exception(RuntimeException.class, this::handle500);
		exception(Exception.class, this::handle500);
	}

	private void handle500(Exception ex, Request req, Response res) {
		setResponse(ex, res, HttpStatus.INTERNAL_SERVER_ERROR_500);
	}

	private void handle400(Exception ex, Request req, Response res) {
		setResponse(ex, res, HttpStatus.BAD_REQUEST_400);
	}

	private void setResponse(Exception ex, Response res, int status) {
		Builder<?> error = new br.com.ocotoenigma.commons.messages.Response.Builder<>()
				.asFail()
				.withMsg(ex.getMessage())
				.withDetails(ExceptionUtils.getStackTrace(ex));

		res.type(APPLICATION_JSON);
		res.status(status);
		res.body(this.toJson(error.build()));
	}
}
