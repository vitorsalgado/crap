var List = require('../../persistence/models/list');
var Response = require('../response');
var Q = require('q');

function ListService() {

    this.add = function (request) {
        var deferred = Q.defer();

        if (!request) {
            deferred.reject();
            return;
        }

        var list = new List({
            userId: request.userId,
            name: request.name,
            description: request.description,
            tags: request.tags,
            pages: request.pages
        });

        list.save(function (error, entity) {
            if (error) {
                deferred.reject(error);
                return;
            }

            deferred.resolve(new Response({
                id: entity._id,
                name: entity.name,
                description: entity.description,
                tags: entity.tags
            }));
        });

        return deferred.promise;
    };

    this.get = function(id){
        var deferred = Q.defer();

        List.findOne({_id: id}, function(error, foundList){
            if (error) {
                deferred.reject(error);
                return;
            }

            deferred.resolve(foundList);
        });

        return deferred.promise;
    };

    this.getByUserId = function (userId) {
        var deferred = Q.defer();

        List.find({userId: userId}, function (error, foundLists) {
            if (error) {
                deferred.reject(error);
                return;
            }

            return deferred.resolve(new Response(processLists(foundLists)));
        });

        return deferred.promise;
    };

    function processLists(foundLists) {
        var lists = [];

        foundLists.forEach(function (list) {
            lists.push({id: list.id, name: list.name, description: list.description, tags: list.tags});
        });

        return lists;
    }
}

module.exports = new ListService();