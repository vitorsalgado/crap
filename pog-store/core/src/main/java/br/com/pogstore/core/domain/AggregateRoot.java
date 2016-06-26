/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.domain;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.function.Consumer;

import br.com.pogstore.core.event.Event;
import br.com.pogstore.core.event.EventProvider;
import br.com.pogstore.core.event.EventStream;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public abstract class AggregateRoot implements EventProvider {

	private final List<Event> uncommitedEvents;
	private final Map<Object, Consumer<Event>> eventRoutes;

	private UUID id;
	private long version;
	private long currentVersion;

	protected AggregateRoot() {
		uncommitedEvents = new ArrayList<>();
		eventRoutes = new HashMap<>();
	}

	@Override
	public UUID getId() {
		return id;
	}

	@Override
	public long getVersion() {
		return version;
	}

	@Override
	public List<Event> getUncommitedEvents() {
		return this.uncommitedEvents;
	}

	@Override
	public void clearEvents() {
		this.uncommitedEvents.clear();
	}

	@Override
	public void loadFromEventStream(EventStream eventStream)
			throws EventNotRegisteredException {
		if (eventStream.isEmpty()) {
			return;
		}

		for (Event event : eventStream.getEvents()) {
			applyEvent(event);
		}

		this.version = eventStream.getLastVersion();
		this.currentVersion = this.version;
	}

	@Override
	public void updateVersion() {
		this.version = this.version + this.currentVersion;
	}

	protected void RaiseEvent(Event event) throws EventNotRegisteredException {
		this.currentVersion++;
		event.setVersion(this.currentVersion);
		event.setEventSourceId(this.id);

		applyEvent(event);
		uncommitedEvents.add(event);
	}

	protected void applyEvent(Event event) throws EventNotRegisteredException {
		Class<?> eventType = event.getClass();

		if (!eventRoutes.containsKey(eventType)) {
			throw new EventNotRegisteredException();
		}

		Consumer<Event> action = eventRoutes.get(eventType);
		action.accept(event);
	}

	protected <T extends Event> void registerEvent(Class<T> type,
			Consumer<T> eventRoute) {
		eventRoutes.put(type, ((Consumer<Event>) eventRoute));
	}
}
