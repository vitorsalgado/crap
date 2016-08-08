package br.com.octoenigma.stream;

public class StreamImpl implements Stream {

	private final String input;
	private int count;

	public StreamImpl(final String input) {
		if (input == null || input.isEmpty()) {
			throw new IllegalArgumentException("Input can't be null or empty.");
		}

		this.input = input;
		this.count = 0;
	}

	public char getNext() {
		return this.input.charAt(count++);
	}

	public boolean hasNext() {
		return count < input.length();
	}

}