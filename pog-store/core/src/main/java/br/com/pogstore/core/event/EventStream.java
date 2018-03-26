package br.com.pogstore.core.event;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class EventStream {

	private UUID eventSourceId;
	private final List<Event> events;
	private long startVersion;
	private long lastVersion;
	private final Boolean isEmpty;

	public EventStream(final UUID eventsourceId, final List<Event> events,
			final long fromVersion, final long toVersion) {
		this.eventSourceId = eventsourceId;
		this.events = events;
		this.startVersion = fromVersion;
		this.lastVersion = toVersion;
		this.isEmpty = false;
	}

	private EventStream() {
		this.events = new ArrayList<>();
		isEmpty = true;
	}

	public UUID getEventSourceId() {
		return eventSourceId;
	}

	public List<Event> getEvents() {
		return events;
	}

	public long getStartVersion() {
		return startVersion;
	}

	public long getLastVersion() {
		return lastVersion;
	}

	public static EventStream empty() {
		return new EventStream();
	}

	public Boolean isEmpty() {
		return isEmpty;
	}

}
