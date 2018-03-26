/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.event;

import java.util.Date;
import java.util.UUID;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public abstract class AbstractEvent implements Event {

	private final UUID identifier;
	private final Date timestamp;
	private String name;
	private UUID eventSourceId;
	private long version;

	protected AbstractEvent() {
		identifier = UUID.randomUUID();
		timestamp = new Date();
	}

	@Override
	public UUID getIdentifier() {
		return identifier;
	}

	@Override
	public String getName() {
		return name;
	}

	@Override
	public String getType() {
		return this.getClass().getCanonicalName();
	}

	@Override
	public UUID getEventSourceId() {
		return eventSourceId;
	}

	@Override
	public void setEventSourceId(UUID id) {
		this.eventSourceId = id;
	}

	@Override
	public Date getTimestamp() {
		return timestamp;
	}

	@Override
	public long getVersion() {
		return version;
	}

	@Override
	public void setVersion(long version) {
		this.version = version;
	}

	@Override
	public String getSourceVersion() {
		return "";
	}

}
