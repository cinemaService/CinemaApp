$(function() {
    var sock = $.connection.messageHub;

    sock.client.send = function(message) {
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page. 
        $('#discussion').append('<li>' + encodedMsg + '</li>');
    };

    $.connection.hub.start().done(function() {
        sock.server.register();
    });
});