FQ.infrastructure.EventManager = {
    trigger: function (context, event) {
        $(context).trigger(event);
    },

    listen: function (context, event, action) {
        $(context).on(event, action);
    }
};
