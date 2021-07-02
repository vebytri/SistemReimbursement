function submitForm() {
    //var formdata = new FormData($('#uploader2').get(0));
    var fileInput = document.getElementById('file');
   // console.log(formdata)
   // console.log(file.files)
    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append('file', file);
    console.log(file)
   

    $.ajax({
        //url: '/home/testupload',
        url: '/home/testupload',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            //rendering success
            console.log(data)
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //rendering errors
            console.log("2")
        }
    });
}