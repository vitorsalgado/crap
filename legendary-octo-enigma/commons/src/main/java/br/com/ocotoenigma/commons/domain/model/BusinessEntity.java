package br.com.ocotoenigma.commons.domain.model;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.MappedSuperclass;
import javax.persistence.Transient;

import br.com.ocotoenigma.commons.domain.rules.BusinessRule;

@MappedSuperclass
public abstract class BusinessEntity {

	@Transient
	private final List<BusinessRule> businessRules = new ArrayList<>();

	@Transient
	private final List<String> validationErrors = new ArrayList<>();

	protected BusinessEntity() {
	}

	protected void addRule(BusinessRule rule) {
		businessRules.add(rule);
	}

	public static void addRule(BusinessEntity be, BusinessRule rule) {
		be.addRule(rule);
	}

	public List<String> getValidationErrors() {
		return this.validationErrors;
	}

	public Boolean validate() {
		validationErrors.clear();

		businessRules.stream()
			.filter((rule) -> rule != null && !rule.validate(this))
			.forEach((rule) -> validationErrors.add(rule.getMessage()));

		return validationErrors.isEmpty();
	}
}
