package br.com.octoenigma.stream;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class AppTest {

	@Test
	public void word_test_must_return_T() {
		String input = "teste";

		char result = App.getFirstNonRepeatingCharacter(input);

		assertEquals('s', result);
	}

	@Test
	public void word_vitor_must_return_V() {
		String input = "vitor";

		char result = App.getFirstNonRepeatingCharacter(input);

		assertEquals('v', result);
	}

	@Test
	public void words_rua_vergueiro_must_return_A() {
		String input = "rua vergueiro";

		char result = App.getFirstNonRepeatingCharacter(input);

		assertEquals("a", Character.toString(result));
	}

	@Test(expected = IllegalArgumentException.class)
	public void stream_must_not_accept_null_values() {
		String input = null;

		new StreamImpl(input);
	}

}
