//Message Utils
var messageNo = 0;

function addMessage() {
    $('.messages').append('<div class="message ' + messageNo + '"><span></span></div>')
    messageNo++;
    return '.message.' + (messageNo - 1)
}

function correctShow(message) {
    var selector = addMessage()
    $(selector + ' span').text(message)
    $(selector)
        .css('background-color', '#21BA45')
        .slideDown('slow')
        .delay(2000)
        .slideUp('slow', function () { $(this).remove() })

}

function errorShow(message) {
    var selector = addMessage()
    $(selector + ' span').text(message)
    $(selector)
        .css('background-color', '#DB2828')
        .slideDown('slow')
        .delay(2000)
        .slideUp('slow', function () { $(this).remove() })
}