/// <reference path="jquery.js" />

var _cpfWithDots = false;
var _hasLocalStorage = false;

function isCpf(cpf) {

	cpf = cpf.replace(/[^\d]+/g, '');

	if (cpf == '')
		return false;

	if (cpf.length != 11 || cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
		return false;

	aux = 0;

	for (i = 0; i < 9; i++)
		aux += parseInt(cpf.charAt(i)) * (10 - i);

	rev = 11 - (aux % 11);

	if (rev == 10 || rev == 11)
		rev = 0;

	if (rev != parseInt(cpf.charAt(9)))
		return false;

	aux = 0;

	for (i = 0; i < 10; i++)
		aux += parseInt(cpf.charAt(i)) * (11 - i);

	rev = 11 - (aux % 11);

	if (rev == 10 || rev == 11)
		rev = 0;

	if (rev != parseInt(cpf.charAt(10)))
		return false;

	return true;
}

function generateNewCpf(cpfWithDots) {
	var cpf = '';

	var n = 9;

	var n1 = randomize(n);
	var n2 = randomize(n);
	var n3 = randomize(n);
	var n4 = randomize(n);
	var n5 = randomize(n);
	var n6 = randomize(n);
	var n7 = randomize(n);
	var n8 = randomize(n);
	var n9 = randomize(n);

	var d1 = n9 * 2 + n8 * 3 + n7 * 4 + n6 * 5 + n5 * 6 + n4 * 7 + n3 * 8 + n2 * 9 + n1 * 10;

	d1 = 11 - (mod(d1, 11));

	if (d1 >= 10)
		d1 = 0;

	var d2 = d1 * 2 + n9 * 3 + n8 * 4 + n7 * 5 + n6 * 6 + n5 * 7 + n4 * 8 + n3 * 9 + n2 * 10 + n1 * 11;

	d2 = 11 - (mod(d2, 11));

	if (d2 >= 10)
		d2 = 0;

	if (cpfWithDots)
		cpf = '' + n1 + n2 + n3 + '.' + n4 + n5 + n6 + '.' + n7 + n8 + n9 + '-' + d1 + d2;
	else
		cpf = '' + n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9 + d1 + d2;

	return cpf;
}

function randomize(n) {
	return Math.round(Math.random() * n);
}

function mod(dividendo, divisor) {
	return Math.round(dividendo - (Math.floor(dividendo / divisor) * divisor));
}

function generateAndFocus() {
	var newCpf = generateNewCpf(_cpfWithDots);

	$('#cpf-generator').val(newCpf);

	$('#cpf-generator').select();
	$('#cpf-generator').focus();
}

function isLocalStorageSupported() {
	var testKey = "test";
	var stg = window.localStorage;

	try {
		stg.setItem(testKey, "1");
		stg.removeItem(testKey);

		return true;
	}
	catch (ex) {
		return false;
	}
}

$(document).ready(function () {

	if (isLocalStorageSupported()) {
		_hasLocalStorage = true;

		var dots = window.localStorage.getItem('use-dots');

		if (dots)
			_cpfWithDots = true;

		if (_cpfWithDots) {
			$('#btn-dots').addClass('btn-red-active');
		} else {
			$('#btn-dots').removeClass('btn-red-active');
		}

	} else {
		$('#btn-dots').hide();
	}

	generateAndFocus();

	$('#btn-generate-cpf').click(function (e) {
		e.preventDefault();
		generateAndFocus();
		$('#invalid-cpf-message').hide();
		$('#valid-cpf-message').hide();
	});

	$('#cpf-generator').keypress(function (e) {
		var keycode = (e.keyCode ? e.keyCode : e.which);
		if (keycode == '13') {
			generateAndFocus();
			$('#invalid-cpf-message').hide();
			$('#valid-cpf-message').hide();
		}
	});

	$('#cpf-generator').click(function (e) {
		$(this).select();
	});

	$('#btn-validate-cpf').click(function (e) {
		e.preventDefault();

		var cpf = $('#cpf-generator').val();

		if (isCpf(cpf)) {
			$('#valid-cpf-message').show();
			$('#invalid-cpf-message').hide();
		} else {
			$('#valid-cpf-message').hide();
			$('#invalid-cpf-message').show();
		}
	});

	if (_hasLocalStorage) {

		$('#btn-dots').click(function (e) {
			e.preventDefault();

			if (_cpfWithDots) {

				_cpfWithDots = false;
				window.localStorage.removeItem('use-dots');
				$(this).removeClass('btn-red-active');

				var cpfFromGenerator = $('#cpf-generator').val();

				if (cpfFromGenerator != null || cpfFromGenerator != '' && isCpf(cpfFromGenerator)) {
					cpfFromGenerator = cpfFromGenerator.replace(/[^\d]+/g, '');
					$('#cpf-generator').val(cpfFromGenerator);
				}

			} else {

				_cpfWithDots = true;
				window.localStorage.setItem('use-dots', true);
				$(this).addClass('btn-red-active');

				var cpfFromGenerator = $('#cpf-generator').val();

				if (cpfFromGenerator != null || cpfFromGenerator != '' && isCpf(cpfFromGenerator)) {

					cpfFromGenerator = cpfFromGenerator.replace(/[^\d]+/g, '');
					cpfFromGenerator = cpfFromGenerator.replace(/(\d{3})(\d)/, "$1.$2");
					cpfFromGenerator = cpfFromGenerator.replace(/(\d{3})(\d)/, "$1.$2");
					cpfFromGenerator = cpfFromGenerator.replace(/(\d{3})(\d)/, "$1-$2");

					$('#cpf-generator').val(cpfFromGenerator);
				}

			}
		});

	}

});
