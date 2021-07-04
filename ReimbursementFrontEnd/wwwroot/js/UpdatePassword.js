function savechange() {
    var obj = new Object();
    obj.Email = $('#email').val();
    obj.Password = $('#passwordori').val();
    $.ajax({
        url: 'https://localhost:44383/api/users/login',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)

    }).done((result) => {

        Swal.fire({
            title: 'Now Loading..',
            //html: 'I will close in <b></b> milliseconds.',
            timer: 1000,
            timerProgressBar: true,
            didOpen: () => {
                Swal.showLoading()
            },

        }).then((result) => {

            obj2 = new Object();
            obj2.Email = obj.Email;
            obj2.Password = $('#newpassword').val();
            console.log(obj2);
           
        })
    }).fail((error) => {
        //alert pemberitahuan jika gagal

        Swal.fire(
            'Failed !',
            'Password is different',
            'error'
        )

        //alert("Data Gagal");
        //console.log(error);
    })


}