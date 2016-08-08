package br.com.octoenigma.address.domain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import br.com.ocotoenigma.commons.domain.model.BusinessEntity;
import br.com.ocotoenigma.commons.domain.rules.RequiredValidator;

@Entity
public class Address extends BusinessEntity {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;

	@Column(nullable = false)
	private String zipCode;

	@Column(nullable = false)
	private String type;

	@Column(nullable = false)
	private String street;

	@Column(nullable = false)
	private String number;

	private String detail;

	@Column(nullable = false)
	private String neighborhood;

	@Column(nullable = false)
	private String city;

	@Column(nullable = false)
	private String state;

	private String reference;

	Address() {
		addRule(new RequiredValidator(this::getZipCode, "CEP é obrigatório"));
		addRule(new RequiredValidator(this::getType, "Tipo de Endereço é obrigatório"));
		addRule(new RequiredValidator(this::getStreet, "Logradouro é obrigatório"));
		addRule(new RequiredValidator(this::getNumber, "Número é obrigatório"));
		addRule(new RequiredValidator(this::getNeighborhood, "Bairro é obrigatório"));
		addRule(new RequiredValidator(this::getCity, "Cidade é obrigatório"));
		addRule(new RequiredValidator(this::getState, "Estado é obrigatório"));
	}

	public Address(String zipCode, String type, String street, String number, String neighborhood, String city,
			String state) {
		this();

		this.zipCode = zipCode;
		this.type = type;
		this.street = street;
		this.number = number;
		this.neighborhood = neighborhood;
		this.city = city;
		this.state = state;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public String getStreet() {
		return street;
	}

	public void setStreet(String street) {
		this.street = street;
	}

	public String getNumber() {
		return number;
	}

	public void setNumber(String number) {
		this.number = number;
	}

	public String getDetail() {
		return detail;
	}

	public void setDetail(String detail) {
		this.detail = detail;
	}

	public String getNeighborhood() {
		return neighborhood;
	}

	public void setNeighborhood(String neighborhood) {
		this.neighborhood = neighborhood;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getState() {
		return state;
	}

	public void setState(String state) {
		this.state = state;
	}

	public String getReference() {
		return reference;
	}

	public void setReference(String reference) {
		this.reference = reference;
	}
}
