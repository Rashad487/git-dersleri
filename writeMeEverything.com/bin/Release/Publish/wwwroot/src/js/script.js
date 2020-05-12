
$(document).ready(function () {


    
    var noti = $.connection.notiHubs;

    noti.client.showNoti = function (message, type) {
    $.notify({
        // options
        message: message
    }, {
            // settings
            type: type
        });
    };





    var message = $.connection.chatHubs;

    message.client.showMessage = function (message, id) {

        var divM = $('<div class="message-item"></div>');
        var divC = $('<div class="message-content"></div>');

        divC.text(message);
        console.log(message);

        var divAc = $('<div class="message-action"></div>');
        divAc.text((new Date()).toISOString().replace(/T/, ' ').replace(/\..+/, ''));
        console.log(divAc);

        divM.append(divC);
        divM.append(divAc);
        var cBody = $(".messages." + id);
        if (cBody != null) {
            cBody.append(divM);
        }

    };


    var online = $.connection.onlineHubs;

    online.client.showOnline = function (id, status) {
        console.log(0);
        var on = $("i.online");
        console.log(on);
        console.log(status);

        if (status == "online") {
            console.log(1);

            on.text(status);
            console.log(10);
        }
        else
        {
            console.log(2);
            on.text("last seen" + status);
           

        }

    };

    $.connection.hub.start().done(function () {

    });





});