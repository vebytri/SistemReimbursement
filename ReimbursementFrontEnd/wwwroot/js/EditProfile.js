const { cwd } = require("process");

function savechange() {
   
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.Nik = $("#nik").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Gender = $("#gender").val();
    obj.BirthDate = $("#birthDate2").val();
    obj.Email = $("#email").val();
    obj.Address = $("#address").val();
    obj.Image = $("#imgdef").val();
    
    var file = document.getElementById("imgprofile");
    console.log(obj.Image);
    if (file.files.length>0) {
        var imgdata = file.files[0];

        var extension = imgdata.name.substring(imgdata.name.lastIndexOf('.') + 1);
        var changename = (Math.floor(Math.random() * Date.now())).toString();
        var uploadname = changename + obj.Nik + '.' + extension;
      
        var new_file = new File([imgdata], uploadname); // createnew file from file, with new name
        console.log(imgdata.name);
        console.log(new_file.name);
        obj.Image = new_file.name;
        console.log(imgdata);
        console.log(obj.Image);
        var formData = new FormData();
        formData.append('file', new_file);//input newfile to local files
        $.ajax({
            //url: '/home/testupload',
            url: '/home/uploadimgprofile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                //rendering success
                console.log(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //rendering errors
                Swal.fire({ title: 'Error', 'text': 'Uploading image went wrong', 'type': 'error' });
                console.log("somethingworng");
            }
        });
    } else {
        console.log("image kosong");
    }
    console.log(obj.Image);
    $.ajax({
        url: 'https://localhost:44383/api/Users/updateuser/',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        beforeSend: function () {
            Swal.showLoading()
        },
        data: JSON.stringify(obj)

    }).done((result) => {
        Swal.hideLoading();
        Swal.fire({ 'title': 'Success', 'text': 'Update Profile Success', 'type': 'success' }).then (function () {
            window.location.reload();
        })
        
    }).fail((error) => {
        Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
        Swal.hideLoading();
    })

}