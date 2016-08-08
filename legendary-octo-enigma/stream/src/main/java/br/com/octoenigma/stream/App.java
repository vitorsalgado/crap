package br.com.octoenigma.stream;

import java.util.Scanner;

public class App {
	public static void main(String[] args) {
		System.out.println("Type some string and press enter:");

		try (Scanner console = new Scanner(System.in)) {
			String input = console.nextLine();

			char firstNonRepeating = getFirstNonRepeatingCharacter(input);

			System.out.println(firstNonRepeating);
		}
	}

	public static char getFirstNonRepeatingCharacter(String input) {
		Stream stream = new StreamImpl(input);

		int[] array = new int[256];
		char firstChar = Character.MIN_VALUE;

		while (stream.hasNext()) {
			array[stream.getNext()]++;
		}

		for (int i = 0; i < input.length(); i++) {
			char charAt = input.charAt(i);

			if (array[charAt] == 1) {
				firstChar = (char) charAt;
				break;
			}
		}

		return firstChar;
	}
}
