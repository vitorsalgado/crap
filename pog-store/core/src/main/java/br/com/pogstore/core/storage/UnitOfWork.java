/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import java.util.UUID;

import br.com.pogstore.core.domain.AggregateRoot;
import br.com.pogstore.core.event.EventProvider;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface UnitOfWork {

	void track(EventProvider eventProvider);

	<T extends AggregateRoot> T get(UUID id);

	void add(AggregateRoot aggregateRoot);

	void commit();

	void rollback();

}
