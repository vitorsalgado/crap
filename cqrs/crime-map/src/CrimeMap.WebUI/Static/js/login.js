window.CrimeMap = window.CrimeMap || {};
CrimeMap.login = CrimeMap.login || {};

CrimeMap.login.signOut = function (antiForgeryToken) {
	var form = document.createElement('form');
	form.action = '/account/signout';
	form.method = 'post';
	form.id = 'headerSignOutForm';

	var input = document.createElement('input');
	input.type = 'hidden';
	input.name = '__RequestVerificationToken';
	input.value = antiForgeryToken;

	form.appendChild(input);

	form.submit();
};
