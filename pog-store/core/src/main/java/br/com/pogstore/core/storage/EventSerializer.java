/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.storage;

import br.com.pogstore.core.event.Event;
import br.com.pogstore.core.event.Snapshot;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface EventSerializer {

	byte[] serializeEvent(Event event);

	Event deserializeEvent(byte[] data);

	Snapshot deserializeSnapshot(byte[] data);

}
