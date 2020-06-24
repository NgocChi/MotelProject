ShowPopup = (url, tilte) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
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
                if (res.isValid) {
                    $("#view-all").html(res.html);
                    $("#myModal .caption-subject .bold .uppercase").html('');
                    $("#myModal .modal-body").html('');
                    $("#myModal").modal('hide');
                    $.notify('Thành công', {
                        globalPosition: 'top-center', className: 'success', offset: {
                            x: 50,
                            y: 100
                        }
                    });

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
    if (confirm("Bạn có chắc chắn muốn xóa ?")) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $("#view-all").html(res.html);
                    $.notify('Xóa Thành công', {
                        globalPosition: 'top-center', className: 'success', offset: {
                            x: 50,
                            y: 100
                        }
                    });
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }
        catch (e) {
            console.log(e);
        }
    }

    return false;
}

