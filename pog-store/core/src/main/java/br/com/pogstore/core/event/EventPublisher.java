/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.event;

import java.util.List;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public interface EventPublisher {

	<T extends Event> void publish(T event);

	void publish(List<Event> eventCollection);

}
