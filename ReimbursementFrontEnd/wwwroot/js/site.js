
    //$("#btnUpload").on('click', function () {
    //        var formData = new FormData();
    //        var fileUpload = $('#files').get(0);
    //        var files = fileUpload.files;
    //        for (var i = 0; i < files.length; {
    //    console.log(files[i].name);
    //            formData.append(files[i].name, files[i]);
    //        }
    //        $.ajax({
    //    url: "http://localhost:59260/service.asmx/Upload",
    //            type: 'POST',
    //            data: formData,
    //            success: function (data) {

    //},
    //            error: function (data) {
    //    alert('error' + data)
    //},
    //            cache: false,
    //            contentType: false,
    //            processData: false,
    //            xhr: function () {
    //                var xhr = new window.XMLHttpRequest();
    //                xhr.upload.addEventListener("progress", function (evt) {
    //                    if (evt.lengthComputable) {
    //                        var percentComplete = Math.round((evt.loaded / evt.total) * 100);
                            
    //                        $('.progress-bar').css('width', percentComplete + '%').attr('aria-valuenow', percentComplete);
    //                        $('.progress-bar').text(percentComplete + '%');
    //                        console.log(percentComplete);
    //                    }
    //                }, false);
    //                return xhr;  
    //            },
    //        });
    //    });