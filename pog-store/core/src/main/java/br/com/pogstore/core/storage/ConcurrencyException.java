/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import java.util.UUID;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public class ConcurrencyException extends Exception {

	private static final long serialVersionUID = 1L;
	private UUID eventSourceId;
	private long eventSourceVersion;

	public ConcurrencyException() {
		super();
	}

	public ConcurrencyException(Throwable cause) {
		super(cause);
	}

	public ConcurrencyException(UUID eventSourceId, long versionToBeSaved) {
		super(
				String.format(
						"There is a newer than %s version of the event source with id %s you are trying to save stored in the event store.",
						versionToBeSaved, eventSourceId));

		this.eventSourceId = eventSourceId;
		this.eventSourceVersion = versionToBeSaved;
	}

	public UUID getEventSourceId() {
		return eventSourceId;
	}

	public long getEventSourceVersion() {
		return eventSourceVersion;
	}

}
