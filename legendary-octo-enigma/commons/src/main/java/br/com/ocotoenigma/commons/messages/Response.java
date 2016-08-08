package br.com.ocotoenigma.commons.messages;

import java.util.ArrayList;
import java.util.List;

public class Response<T> {
	protected Boolean success = true;
	protected String message = "";
	protected String details = "";
	protected List<String> validationErrors = new ArrayList<>();
	protected T data;

	public Response() {

	}

	public Boolean isSuccess() {
		return success;
	}

	public void setSuccess(Boolean success) {
		this.success = success;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public String getDetails() {
		return this.details;
	}

	public void setDetails(String details) {
		this.details = details;
	}

	public List<String> getValidationErrors() {
		return validationErrors;
	}

	public void setValidationErrors(List<String> validationErrors) {
		this.validationErrors = validationErrors;
	}

	public T getData() {
		return this.data;
	}

	public void setData(T data) {
		this.data = data;
	}

	public static class Builder<T> {
		private Boolean success;
		private String message;
		private String details;
		private List<String> validationErrors;
		private T data;

		public Builder<T> asSuccess() {
			this.success = true;
			return this;
		}

		public Builder<T> asFail() {
			this.success = false;
			return this;
		}

		public Builder<T> withMsg(String message) {
			this.message = message;
			return this;
		}

		public Builder<T> withDetails(String details) {
			this.details = details;
			return this;
		}

		public Builder<T> withValidationErrors(List<String> validationErrors) {
			this.validationErrors = validationErrors;
			return this;
		}

		public Builder<T> withData(T data) {
			this.data = data;
			return this;
		}

		public Response<T> build() {
			return new Response<T>(this);
		}
	}

	private Response(Builder<T> builder) {
		this.success = builder.success;
		this.message = builder.message;
		this.details = builder.details;
		this.validationErrors = builder.validationErrors;
		this.data = builder.data;
	}
}
