(function ($) {
    "use strict";

    var request;
    flatpickr(document.getElementById('due-date'), {
        enableTime: true,
        dateFormat: "d M Y",
        minDate: new Date()
    });

    $('.request-menu a').on('click', function () {
        $('.request-menu a').removeClass('active');
        $(this).addClass('active');
        $('.request').hide();
        $('.' + $(this).data("requesttype")).show(500);
        return false;
    });

    $(".requests").on("click", ".delete-request", function () {
        $(this).closest('.request').addClass('outline-badge-danger');
        $(this).closest('.request').slideUp(550, function () {
            $(this).closest('.request').remove();
        });
    });


    //$(".requests").on("click", ".edit-request", function () {
    //    request = $(this).closest('.request');
    //    $('#lblRequestID').val($(this).closest('.request').find('.request-due-date').html());
    //    $('#lblRequestBy').val($(this).closest('.request').find('.request-due-date').html());
    //    $('#lblStatus').val($(this).closest('.request').find('.request-due-date').html());
    //    $('#lblUpdateBy').val($(this).closest('.request').find('.request-due-date').html());
    //    $('#txtComment').html($(this).closest('.request').find('.request-due-date').html());

    //    $('#status').val($(this).closest('.request').find('.request-content').data('status'));
    //    $("#editrequest").modal();

    //});

    
    
    $(".edit-request-form").submit(function (event) {
        var duedate = $('#due-date').val();
        var status = $('#status').val();
        request.find('.request-due-date').html(duedate);
        request.removeClass('paid-request');
        request.removeClass('pending-request');
        request.removeClass('canceled-request');
        request.addClass(status);
        request.find('.request-content').data('status', status);
        $('#editrequest').modal('toggle');
        event.preventDefault();
    });

    $(".request-search").on("keyup", function () {
        var v = $(".request-search").val().toLowerCase();
        var rows = $('.' + $('.request-menu li a.active').data("requesttype"));

        for (var i = 0; i < rows.length; i++) {
            var fullname = rows[i].getElementsByClassName("request-content");
            fullname = fullname[0].innerHTML.toLowerCase();
            if (fullname) {
                if (v.length == 0 || (v.length < 1 && fullname.indexOf(v) == 0) || (v.length >= 1 && fullname.indexOf(v) > -1)) {
                    rows[i].style.animation = 'fadein 7s';
                    rows[i].style.display = "block";
                } else {
                    rows[i].style.display = "none";
                    rows[i].style.animation = 'fadeout 7s';
                }
            }
        }
    });

    $(".request-info").on("click", function () {
        $('.inv-no').html($(this).closest('.request').find('.request-no').html());

        $('.view-request').fadeIn(1000);
    });
    $(".back-to-request").on("click", function () {

        $('.view-request').fadeOut();
    });

})(jQuery);



