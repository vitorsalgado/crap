package br.com.octoenigma.address.infrastructure.settings;

public class Settings {
	private String zipCodeServiceEndpoint;
	private int port;

	public String getZipCodeServiceEndpoint() {
		return zipCodeServiceEndpoint;
	}

	public void setZipCodeServiceEndpoint(String zipCodeServiceEndpoint) {
		this.zipCodeServiceEndpoint = zipCodeServiceEndpoint;
	}

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}
}
