function ApiAccessTokenPayload(userId, fbUserId, application) {
    this.application = application;
    this.facebookUserId = fbUserId;
    this.userId = userId;
};

module.exports = ApiAccessTokenPayload;
