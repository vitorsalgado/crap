package br.com.pogstore.core.event;

import java.util.Date;
import java.util.UUID;

public interface Event {

	UUID getIdentifier();

	String getName();

	String getType();

	UUID getEventSourceId();

	void setEventSourceId(UUID id);

	Date getTimestamp();

	long getVersion();

	void setVersion(long version);

	String getSourceVersion();

}
