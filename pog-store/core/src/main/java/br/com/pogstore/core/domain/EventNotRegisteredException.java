/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.pogstore.core.domain;

import java.util.List;

import br.com.pogstore.core.event.Event;

/**
 *
 * @author Vitor Hugo Salgado <vsalgadopb@gmail.com>
 */
public class EventNotRegisteredException extends RuntimeException {

	private static final long serialVersionUID = 1L;
	private String message = null;

	public EventNotRegisteredException() {
		super();
	}

	public EventNotRegisteredException(Throwable cause) {
		super(cause);
	}

	public EventNotRegisteredException(Event event, List<Event> registeredEvents) {

		StringBuilder str = new StringBuilder();

		str.append(String.format(
				"Then event \"%s\" is not registered in the Aggregate Root.",
				event.toString()));

		str.append("\n");
		str.append("Current Registered Events in Aggregate Root:");
		str.append("\n");

		for (Event e : registeredEvents) {
			str.append("*  ").append(e.toString()).append("  *").append("\n");
		}

		this.message = str.toString();
	}

	public EventNotRegisteredException(Event event) {
		StringBuilder str = new StringBuilder();

		str.append(String.format(
				"Then event \"%s\" is not registered in the Aggregate Root.",
				event.toString()));

		str.append("\n");

		this.message = str.toString();
	}

	@Override
	public String toString() {
		return message;
	}

	@Override
	public String getMessage() {
		return message;
	}

}
