ShowPopup = (url,tilte) =>
{
    try {
        $.ajax({
            //type: "GET",
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
                if (res.IsValid) {
                    $("view-all").html(res.html);
                    $('#myModal .caption-subject .bold .uppercase').html('');
                    $('#myModal .modal-body').html('');
                    $('#myModal').modal('show');
                }
                else
                    $('#myModal .modal-body').html(res.html);
            },
            error: function (err) {

            }
        })
    }
    catch(e){

    }
    return false;
}