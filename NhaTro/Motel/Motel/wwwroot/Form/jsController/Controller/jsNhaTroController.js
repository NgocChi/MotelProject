ShowPopup = (url,tilte) =>
{
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                console.log(res);
                $("#myModal .caption-subject .bold .uppercase").html(tilte);
                $("#myModal .modal-body").html(res);
                $("#myModal").modal('show');
            }
        });
    }
    catch (e) {
        console.log(e);
    }
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.IsValid) {
                    $("#view-all").html(res.html);
                    $("#myModal .caption-subject .bold .uppercase").html('');
                    $("#myModal .modal-body").html('');
                    $("#myModal").modal('hide');
                    $("#myModal").show(false);
                }
                else
                    $('#myModal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

jQueryAjaxDelete = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $("#view-all").html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}