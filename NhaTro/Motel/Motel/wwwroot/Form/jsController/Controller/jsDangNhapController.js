jQueryAjaxLogin = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $.notify('Đăng nhập thành công', {
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
    return false;
}