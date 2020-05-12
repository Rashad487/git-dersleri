(function ($) {

  




    setTimeout(checkCookie, 1000);

    // check  cookie  , if exists message add notification
    function checkCookie() {
        if (typeof $.cookie('message') !== 'undefined') {

            var mes = $.cookie("message");
            $.removeCookie('message', { path: '/' });
            $.cookie("message", "", { expires: -1 });
            $.notify({
                // options
                message: mes
            }, {
                    // settings
                    type: 'info'
                }
            );
        }
    }

    // click friend name for open chat

    $(".friendlist").on("click",function () { 

        var Id = $(this).attr("id");
        $.ajax({
            url: "/messenger/chat?Id=" + Id,
            type: "get",
            dataType: "html",
            success: function (response) {
                $(".chatandabout").empty();
                $(".chatandabout").html(response);

            },
            error: function (error) {
            }
        });

        $.ajax({
            url: "/messenger/AboutUser?Id=" + Id,
            type: "get",
            dataType: "html",
            success: function (response) {
                $(".aboutuser").html(response);

            },
            error: function (error) {
            }
        });



   





    });

    //Accepst Or Delete Friend Reguest
    $(document).on("click", ".acceptfriend", function (e) {
        e.preventDefault()
        var operation = "accept";

        var Id = $(this).parent().attr("id");

        frends(Id, operation);

        
    });


    $(document).on("click", ".deletefriend", function (e) {
        e.preventDefault();
        var operation = "delete";

        var Id = $(this).parent().attr("id");

        frends(Id, operation);

    });


    function frends( Id, Operation)
    {
        $.ajax({
            url: "/messenger/AcceptOrDeleteFriend?Id=" + Id + "&Opr=" + Operation,
            type: "get",
            dataType: "html",
            success: function (response) {
                $(".reguestfr").empty();

            },
            error: function (error) {
            }
               
        });
        $(".close").click();
        setTimeout(checkCookie, 1000);
    }


    $(".notfriendbtn").on("click", function (e) {
        e.preventDefault();
        $.ajax({
            url: "/messenger/notfriend",
            type: "get",
            dataType: "html",
            success: function (response) {
                $(".notfriend").empty();
                $(".notfriend").html(response);

            },
            error: function (error) {
            }
        });
    });
    // edit profile
    


    
    
})(jQuery);