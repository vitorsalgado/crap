package br.com.ocotoenigma.commons.rest;

import com.google.gson.Gson;

public abstract class RestController {
	protected final static String APPLICATION_JSON = "application/json";
	
	private Gson gson = new Gson();

	public abstract void buildRoutes();

	protected String toJson(Object model) {
		return gson.toJson(model);
	}

	protected Object fromJson(Class<?> type, String data) {
		return gson.fromJson(data, type);
	}
}
