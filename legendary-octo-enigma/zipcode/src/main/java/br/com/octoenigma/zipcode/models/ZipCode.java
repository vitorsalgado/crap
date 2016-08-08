package br.com.octoenigma.zipcode.models;

import javax.persistence.Entity;

@Entity
public class ZipCode {

	private long id;
	private String code;
	private String street;
	private String neighborhood;
	private String city;
	private String state;

	public ZipCode() {

	}

	public ZipCode(long id, String code, String street, String neighborhood, String city, String state) {
		this.id = id;
		this.code = code;
		this.street = street;
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

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public String getStreet() {
		return street;
	}

	public void setStreet(String street) {
		this.street = street;
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
}
