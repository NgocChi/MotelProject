Selected = (url) => {
    try {
        $.ajax({
            type: "GET",
            url: url,
            data: {},
            success: function (data) {
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