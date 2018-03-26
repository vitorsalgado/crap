/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import java.util.UUID;

import br.com.pogstore.core.domain.AggregateRoot;
import br.com.pogstore.core.event.EventStream;
import br.com.pogstore.core.event.Originator;
import br.com.pogstore.core.event.Snapshot;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public class DomainRepositoryImpl implements DomainRepository {

	private final EventStore eventStore;

	public DomainRepositoryImpl(EventStore eventStore) {
		this.eventStore = eventStore;
	}

	@SuppressWarnings("unchecked")
	@Override
	public <T extends AggregateRoot> T get(UUID id) {

		Snapshot snapshot = eventStore.getSnapshot(id);
		AggregateRoot aggregate;

		try {
			aggregate = AggregateRoot.class.newInstance();
		} catch (Exception e) {
			throw new RuntimeException(e.getMessage(), e);
		}

		long maxVersion = Long.MAX_VALUE;
		long minVersion = Long.MIN_VALUE;

		if (snapshot != null) {
			minVersion = snapshot.getVersion() + 1;

			if (aggregate instanceof Originator) {
				((Originator) aggregate).setSnapshot(snapshot);
			}
		}

		EventStream eventStream = eventStore.getEvents(id, minVersion,
				maxVersion);

		if (eventStream.isEmpty()) {
			return null;
		}

		aggregate.loadFromEventStream(eventStream);

		return ((T) aggregate);
	}

	public void store(AggregateRoot aggregateRoot) {
		eventStore.store(aggregateRoot);
	}

}
