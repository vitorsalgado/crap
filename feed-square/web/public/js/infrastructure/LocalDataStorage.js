FQ.infrastructure.LocalDataStorage = (function () {
    var storage = null;
    var memoryDataStorage = {
        _data: {},

        setItem: function (key, item) {
            this._data[key] = String(item);
        },

        getItem: function (key) {
            return this._data.hasOwnProperty(key) ? this._data[key] : undefined;
        },

        removeItem: function (key) {
            delete this._data[key];
        },

        clear: function () {
            this._data = {};
        }

    };

    if (isLocalStorageSupported()) {
        storage = window.localStorage;
    } else {
        if (!window.memoryStorage) {
            storage = window.memoryStorage = memoryDataStorage;
        } else {
            storage = window.memoryStorage;
        }
    }

    return {
        setItem: function (key, item) {
            storage.setItem(key, item);
        },

        getItem: function (key) {
            return this.storage.getItem(key);
        },

        removeItem: function (key) {
            this.storage.removeItem(key);
        },

        clear: function () {
            this.storage.clear();
        }
    };

    function isLocalStorageSupported() {
        var testKey = "fq.infrastructure.storage.test";
        var stg = window.localStorage;

        try {
            stg.setItem(testKey, "1");
            stg.removeItem(testKey);

            return true;
        } catch (ex) {
            console.log(ex);
            return false;
        }
    }

})();
