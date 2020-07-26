ShowPopup = (url, tilte) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $("#myModal .modal-header .caption  .caption-subject ").html(tilte);
                $("#myModal .modal-body").html(res);
                $("#myModal").modal('show');
            }
        });
    }
    catch (e) {
        console.log(e);
    }
}

ShowPopupLarge = (url, tilte) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $("#myModallarge .caption-subject .bold .uppercase").html(tilte);
                $("#myModallarge .modal-body").html(res);
                $("#myModallarge").modal('show');
            }
        });
    }
    catch (e) {
        console.log(e);
    }
}

ShowPopupSmall = (url, tilte) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $("#myModalsmall .modal-header .caption  .caption-subject").html(tilte);
                $("#myModalsmall .modal-body").html(res);
                $("#myModalsmall").modal('show');
            }
        });
    }
    catch (e) {
        console.log(e);
    }
}

jQueryAjaxPostSmall = form => {
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
                    $("#myModalsmall .caption-subject .bold .uppercase").html('');
                    $("#myModalsmall .modal-body").html('');
                    $("#myModalsmall").modal('hide');
                    $("#myModalsmall").modal('hide');
                    $.notify('Thành công', {
                        globalPosition: 'top-center', className: 'info', offset: {
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
                    $("#myModal").modal('hide');
                    $.notify('Thành công', {
                        globalPosition: 'top-center', className: 'info', offset: {
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

jQueryAjaxPostdemo = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#myModal .caption-subject .bold .uppercase").html('');
                    $("#myModal .modal-body").html('');
                    $("#myModal").modal('hide');
                    $("#myModal").modal('hide');
                    $("#chooseNhaTro").modal('hide');
                    $.notify('Thành công', {
                        globalPosition: 'top-center', className: 'info', offset: {
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

jQueryAjaxPostLarge = form => {
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
                    $("#myModallarge .caption-subject .bold .uppercase").html('');
                    $("#myModallarge .modal-body").html('');
                    $("#myModallarge").modal('hide');
                    $("#myModallarge").modal('hide');
                    $.notify('Thành công', {
                        globalPosition: 'top-center', className: 'info', offset: {
                            x: 50,
                            y: 100
                        }
                    });

                }
                else
                    $('#myModallarge .modal-body').html(res.html);
                    $.notify('Thất bại', {
                        globalPosition: 'top-center', className: 'info', offset: {
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
                    if (res.isValid) {
                        $("#view-all").html(res.html);
                        $.notify('Xóa Thành công', {
                            globalPosition: 'top-center', className: 'success', offset: {
                                x: 50,
                                y: 100
                            }
                        });
                    }
                    else {
                        $("#view-all").html(res.html);
                        $.notify('Đang được sử dụng không thể xóa', {
                            globalPosition: 'top-center', className: 'denger', offset: {
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
    }

    return false;
}

$(function () {
    $('#loadbody').addClass('hide');
    $(document).bind('ajaxStart', function () {
        $('#loadbody').removeClass('hide');

    }).bind('ajaxStop', function () {
        $('#loadbody').addClass('hide');
    })

});

jQueryAjaxChooseTTP = (url) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $("#table_phong").html(res.html);
            }
        });
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

jQueryAjaxSaveChange = form => {
    if (confirm("Bạn có chắc chắn muốn lưu thây đổi ?")) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $("#table_phong").html(res.html);
                        $.notify('Lưu Thành công', {
                            globalPosition: 'top-center', className: 'success', offset: {
                                x: 50,
                                y: 100
                            }
                        });
                    }
                    else {
                        $("#table_phong").html(res.html);
                        $.notify('Lưu thất bại', {
                            globalPosition: 'top-center', className: 'denger', offset: {
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
    }

    return false;
}


jQueryAjaxExportPDF = (url) => {
    try {

        window.open(url, '_blank');
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

