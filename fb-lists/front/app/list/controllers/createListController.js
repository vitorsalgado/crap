angular
    .module(NS.modules.list)
    .controller('CreateListController1', CreateListController1);

function CreateListController1($scope, $rootScope, $uibModalInstance, ListService) {
    $scope.next = function () {
        var user = $rootScope.user;

        var list = {
            name: $scope.listName,
            description: $scope.listDescription,
            tags: $scope.listTags,
            userId: user.id
        };

        $uibModalInstance.close(list);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };
}

angular
    .module(NS.modules.list)
    .controller('CreateListController2', CreateListController2);

function CreateListController2($scope, $rootScope, $uibModalInstance, list, PageService, ListService) {

    $scope.allPages = [];
    $scope.addedPages = [];
    $scope.currentPage = 1;
    $scope.pagedData = {};
    $scope.pages = [];

    PageService.getLikes()
        .then(function (response) {
            $scope.allPages = response;

            var curPage = 1;
            var index = -1;
            var pageSize = 6;
            var pageCount = Math.ceil($scope.allPages.length / pageSize);
            var result = {pageCount: pageCount, total: $scope.allPages.length, data: []};

            for (var i = 0; i < $scope.allPages.length; i++) {
                var page = $scope.allPages[i];
                index++;

                if (result.data[curPage] == null || typeof result.data[curPage] == 'undefined') {
                    result.data[curPage] = [];
                }

                result.data[curPage].push(page);

                if (index == pageSize - 1) {
                    index = -1;
                    curPage++;
                }
            }

            $scope.pagedData = result;
            $scope.pages = result.data[$scope.currentPage];

            $rootScope.$broadcast('app.pagesLoaded');
        });

    $scope.getPage = function (pageIndex) {
        $scope.pages = $scope.pagedData.data[pageIndex];
        $scope.currentPage = pageIndex;
        $rootScope.$broadcast('app.pagesLoaded');
    };

    $scope.search = function () {

    };

    $scope.addPage = function (page) {
        if ($scope.addedPages.length == 0) {
            $scope.addedPages.push(page);
            return;
        }

        var canAdd = true;

        for (var i = 0; i < $scope.addedPages.length; i++) {
            var p = $scope.addedPages[i];

            if (page.id == p.id) {
                canAdd = false;
                break;
            }
        }

        if (canAdd) {
            $scope.addedPages.push(page);
        }
    };

    $scope.removePage = function (id) {
        angular.forEach($scope.addedPages, function (page, index) {
            if (page.id == id) {
                $scope.addedPages.splice(index, 1);
            }
        });
    };

    $scope.add = function () {
        $rootScope.loading = true;
        list.pages = [];

        for (var i = 0; i < $scope.addedPages.length; i++) {
            list.pages.push($scope.addedPages[i].id);
        }

        ListService.add(list)
            .then(
            function (response) {
                $uibModalInstance.close(response);
            },
            function () {
                $rootScope.loading = false;
            }
        );
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };

}