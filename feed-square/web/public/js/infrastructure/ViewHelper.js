FQ.infrastructure.ViewHelper = {

    hideLoadingOverlay: function () {
        $('#loading-overlay').hide();
    },

    openModal: function (elementQuerySelector, options) {
        $(elementQuerySelector).modal(options);
    }
};
