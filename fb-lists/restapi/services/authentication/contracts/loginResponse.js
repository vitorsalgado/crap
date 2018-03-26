function LoginResponse(accountDto, accessToken) {
    this.account = accountDto;
    this.token = accessToken;
}

module.exports = LoginResponse;