var Config = {
    bots: {
        google: {
            name: "Jedi Bot",
            jid: "jedi.robott@gmail.com",
            password: process.env.JEDI_PASSWORD,
            host: "talk.google.com",
            port: 5222,
            keepAliveInterval: 15000
        }
    }
};

module.exports = Config;