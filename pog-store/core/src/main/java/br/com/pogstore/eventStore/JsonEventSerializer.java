package br.com.pogstore.eventStore;

import java.nio.charset.StandardCharsets;

import br.com.pogstore.core.event.Event;
import br.com.pogstore.core.event.Snapshot;
import br.com.pogstore.core.storage.EventSerializer;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class JsonEventSerializer implements EventSerializer {

	@Override
	public byte[] serializeEvent(Event event) {
		ObjectMapper mapper = new ObjectMapper();
		String json;

		try {
			json = mapper.writeValueAsString(event);
		} catch (JsonProcessingException e) {
			throw new RuntimeException(e.getMessage(), e);
		}

		byte[] bytes = json.getBytes(StandardCharsets.US_ASCII);

		return bytes;
	}

	@Override
	public Event deserializeEvent(byte[] data) {

		ObjectMapper mapper = new ObjectMapper();
		String decodedData = new String(data, StandardCharsets.US_ASCII);
		Event event;

		try {
			event = mapper.readValue(decodedData, Event.class);
		} catch (Exception e) {
			throw new RuntimeException(e.getMessage(), e);
		}

		return event;
	}

	@Override
	public Snapshot deserializeSnapshot(byte[] data) {

		ObjectMapper mapper = new ObjectMapper();
		String decodedData = new String(data, StandardCharsets.US_ASCII);
		Snapshot snapshot;

		try {
			snapshot = mapper.readValue(decodedData, Snapshot.class);
		} catch (Exception e) {
			throw new RuntimeException(e.getMessage(), e);
		}

		return snapshot;
	}

}
