$(function() {
    var sock = $.connection.messageHub;

    sock.client.send = function(message) {
        $('.modal-body').text(message);
        $('#myModal').modal('show');
    };

    $.connection.hub.start().done(function() {
        sock.server.register();
    });
});