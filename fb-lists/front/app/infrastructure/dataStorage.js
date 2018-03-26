angular
    .module(NS.modules.infrastructure)
    .service('DataStorage', DataStorage);

function DataStorage() {
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

    this.setItem = function (key, item) {
        storage.setItem(key, item);
    };

    this.getItem = function (key) {
        return this.storage.getItem(key);
    };

    this.removeItem = function (key) {
        this.storage.removeItem(key);
    };

    this.clear = function () {
        this.storage.clear();
    };

    function isLocalStorageSupported() {
        var testKey = "app.infrastructure.storage.test";
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
}