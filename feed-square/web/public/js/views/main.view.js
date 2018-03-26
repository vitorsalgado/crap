FQ.views.Main = (function () {
    var eventManager = FQ.infrastructure.EventManager;
    var viewHelper = FQ.infrastructure.ViewHelper;
    var globals = FQ.infrastructure.Globals;
    var pagesController = FQ.controllers.FbPagesController;
    var accountController = FQ.controllers.AccountController;

    var pageLikes = null;
    var currentPage = 1;

    return {
        init: function () {
            eventManager.listen('#btn-profile', 'click', function (e) {
                e.preventDefault();

                document.querySelector('#avatar > img').src = globals.user.picture;
                document.querySelector('#profile-information > h2').innerHTML = globals.user.name;

                viewHelper.openModal('#dialog-profile');
            });

            eventManager.listen('#btn-sign-out', 'click', function (e) {
                accountController.signOut();
            });

            eventManager.listen('#btn-add-column', 'click', function (e) {
                e.preventDefault();

                pagesController.getPageLikes()
                    .then(function (response) {
                        buildPageGrid(response, function (result) {
                            console.log(result);
                            pageLikes = result;

                            var page = pageLikes[currentPage];
                            var pagesContainer = document.getElementById('pages-paged-container');

                            for (var i = 0; i < page.length; i++) {
                                var item = page[i];

                                var a = document.createElement('a');
                                a.href = 'javascript:void(0);';
                                a.className = 'list-group-item';

                                var h4 = document.createElement('h4');
                                h4.className = 'list-group-item-heading';
                                h4.innerHTML = item.name;

                                var p = document.createElement('p');
                                p.className = 'list-group-item-text';
                                p.innerHTML = item.about;

                                a.appendChild(h4);
                                a.appendChild(p);

                                pagesContainer.appendChild(a);
                            }
                        });
                    });
            });
        }
    };

    function buildPageGrid(pages, callback) {
        var pageSize = FQ.infrastructure.Settings.defaultLikesPageSize;
        var pageCount = Math.ceil(pages.length / pageSize);
        var currentPage = 1;
        var index = -1;
        var result = {
            pageCount: pageCount,
            total: pages.length
        };

        for (var i = 0; i < pages.length; i++) {
            var page = pages[i];
            index++;

            if (result[currentPage] == null || typeof result[currentPage] == 'undefined') {
                result[currentPage] = [];
            }

            result[currentPage].push(page);

            if (index == pageSize - 1) {
                index = -1;
                currentPage++;
            }
        }

        callback(result);
    }

})();
