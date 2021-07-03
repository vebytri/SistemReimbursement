
function forgotpass() {
    let email = $("#email").val();
    let pass = $("#password").val();
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.email = email;
    obj.password = pass;
    console.log(obj);
        $.ajax({
            url: 'https://localhost:44383/api/Users/forgotpassword',
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
            Swal.fire({ title: 'Success', 'text': ('Reset Password Success Please Cek Your Email'), 'type': 'success' })
            Swal.hideLoading();
        }).fail((error) => {
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            Swal.hideLoading();

        })

}