function DeleteUrl(shorterUrl) {
    $.ajax({
        url: "/Url/DeleteUrl",
        type: "Delete",
        datatype: "Json",
        data: { shorterUrl: shorterUrl },
        success: function (result) {
            window.location.reload();
        }
        // Move to a new location or you can do something else

    });
}
