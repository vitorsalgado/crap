package br.com.ocotoenigma.commons.domain.rules;

import java.util.function.Supplier;

import br.com.ocotoenigma.commons.domain.model.BusinessEntity;

public class RequiredValidator extends BusinessRule {

	public RequiredValidator(Supplier<Object> func) {
		super(func, "required");
	}

	public RequiredValidator(Supplier<Object> func, String errorMessage) {
		super(func, errorMessage);
	}

	@Override
	public int getIdentifier() {
		return 1;
	}

	@Override
	public Boolean validate(BusinessEntity entity) {
		Object data = supplier.get();

		if (data == null || data.toString().isEmpty()) {
			return false;
		}

		return true;
	}

}