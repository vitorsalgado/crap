package br.com.ocotoenigma.commons.domain.exceptions;

public class DomainException extends RuntimeException {
	private static final long serialVersionUID = 5582423423720492323L;
	private String message;

	public DomainException(String message) {
		this.message = message;
	}

	@Override
	public String getMessage() {
		return this.message;
	}
}
