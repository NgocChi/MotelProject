jQueryAjaxLogin = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $.notify('Đăng nhập thành công, hãy chọn nhà trọ', {
                        globalPosition: 'top-center', className: 'success', offset: {
                            x: 50,
                            y: 100
                        }
                    });
                    $("#chooseNhaTro .modal-body").html(res.html);
                    $("#chooseNhaTro").modal('show');

                } else {
                    $.notify('Đăng nhập thất bại, kiểm trai lại tài khoản', {
                        globalPosition: 'top-center', className: 'error', offset: {
                            x: 50,
                            y: 100
                        }
                    });
                }
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
                    $("#chooseNhaTro .modal-body").html(res.html);
                    $("#chooseNhaTro").modal('show');
                }
                else {
                    $("#view-all").html(res.html);
                }

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
