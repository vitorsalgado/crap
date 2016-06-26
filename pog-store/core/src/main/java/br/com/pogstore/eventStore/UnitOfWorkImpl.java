package br.com.pogstore.eventStore;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import br.com.pogstore.core.domain.AggregateRoot;
import br.com.pogstore.core.event.EventProvider;
import br.com.pogstore.core.event.EventPublisher;
import br.com.pogstore.core.storage.DomainRepository;
import br.com.pogstore.core.storage.EventStore;
import br.com.pogstore.core.storage.UnitOfWork;

public class UnitOfWorkImpl implements UnitOfWork {

	private EventStore eventStore;
	private DomainRepository domainRepository;
	private EventPublisher eventPublisher;
	private List<EventProvider> trackedEventProviders;

	public UnitOfWorkImpl(EventStore eventStore,
			DomainRepository domainRepository, EventPublisher eventPublisher) {
		this.eventStore = eventStore;
		this.domainRepository = domainRepository;
		this.eventPublisher = eventPublisher;
		this.trackedEventProviders = new ArrayList<EventProvider>();
	}

	@Override
	public void track(EventProvider eventProvider) {
		Boolean isContained = false;

		for (EventProvider evt : trackedEventProviders) {
			if (evt.getId().equals(eventProvider.getId())) {
				isContained = true;
				break;
			}
		}

		if (isContained) {
			return;
		}

		trackedEventProviders.add(eventProvider);
	}

	@Override
	public <T extends AggregateRoot> T get(UUID id) {
		T aggregate = domainRepository.get(id);

		track(aggregate);

		return aggregate;
	}

	@Override
	public void add(AggregateRoot aggregateRoot) {
		track(aggregateRoot);
	}

	@Override
	public void commit() {
		try {
			for (EventProvider eventProvider : trackedEventProviders) {
				eventStore.store(eventProvider);
				eventPublisher.publish(eventProvider.getUncommitedEvents());
				eventProvider.clearEvents();
			}

			trackedEventProviders.clear();

		} catch (Exception ex) {
			rollback();
			throw ex;
		}
	}

	@Override
	public void rollback() {
		trackedEventProviders.clear();
	}

}
