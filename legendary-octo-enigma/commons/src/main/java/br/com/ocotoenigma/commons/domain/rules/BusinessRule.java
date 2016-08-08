package br.com.ocotoenigma.commons.domain.rules;

import java.util.function.Supplier;

import br.com.ocotoenigma.commons.domain.model.BusinessEntity;

public abstract class BusinessRule {
	private final String message;
	protected final Supplier<Object> supplier;

	protected BusinessRule(Supplier<Object> func) {
		this(func, "");
	}

	protected BusinessRule(Supplier<Object> func, String errorMessage) {
		this.supplier = func;
		this.message = errorMessage;
	}

	public String getMessage() {
		return message;
	}

	public abstract int getIdentifier();

	public abstract Boolean validate(BusinessEntity entity);
}