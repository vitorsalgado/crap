package br.com.pogstore.core.event;

import java.util.List;
import java.util.UUID;

import br.com.pogstore.core.domain.EventNotRegisteredException;

public interface EventProvider {

	UUID getId();

	long getVersion();

	List<Event> getUncommitedEvents();

	void clearEvents();

	void loadFromEventStream(EventStream eventStream)
			throws EventNotRegisteredException;

	void updateVersion();

}
