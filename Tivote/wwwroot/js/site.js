$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    });
    $('a[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    })
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = new FormData(form.get(0));
        console.log(sendData);
        $.ajax({
            url: actionUrl,
            method: 'post',
            data: sendData,
            backdrop: 'static',
            keyboard: false,
            processData: false,
            contentType: false,
            cache: false,
            success: function (redata) {
                console.log(redata);
                if (redata.status === "success") {
                    PlaceHolderElement.find('.modal').modal('hide');
                }
                else {
                    var newBody = $('.modal-body', redata);
                    var newFoot = $('.modal-footer', redata);
                    PlaceHolderElement.find('.modal-body').replaceWith(newBody);
                    PlaceHolderElement.find('.modal-footer').replaceWith(newFoot);
                }
            },
            error: function (message) {
                alert(message);
            }
        })
    })
})