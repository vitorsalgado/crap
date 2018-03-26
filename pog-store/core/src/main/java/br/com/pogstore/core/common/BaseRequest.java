package br.com.pogstore.core.common;

import java.util.UUID;

public abstract class BaseRequest {

	private String identifier;
	
	protected BaseRequest() {
		this.identifier = UUID.randomUUID().toString();
	}

	protected BaseRequest(String identifier) {
		this.identifier = identifier;
	}

	protected BaseRequest(UUID identifier){
		this(identifier.toString());
	}
	
	public String getIdentifier() {
		return identifier;
	}
	
}
