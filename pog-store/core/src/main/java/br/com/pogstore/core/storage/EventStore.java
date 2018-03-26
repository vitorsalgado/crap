/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import java.util.UUID;

import br.com.pogstore.core.event.EventProvider;
import br.com.pogstore.core.event.EventStream;
import br.com.pogstore.core.event.Snapshot;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface EventStore {

	EventStream getEvents(UUID id, long minVersion, long maxVersion);

	void store(EventProvider eventProvider);

	Snapshot getSnapshot(UUID id);

	void saveSnapshot(Snapshot snapshot);

}
