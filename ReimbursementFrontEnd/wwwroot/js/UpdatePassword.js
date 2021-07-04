function savechange() {
    var obj = new Object();
    obj.Email = $('#email').val();
    obj.Password = $('#passwordori').val();
    var obj2 = new Object();
    obj2.Email = $('#email').val();
    obj2.Password = $('#newpassword').val();
    $.ajax({
        beforeSend: function () {
            Swal.showLoading()
        },
        url: 'https://localhost:44383/api/users/cekpassword',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj),
        success: function (result) {
            console.log(result);
            $.ajax({

                url: 'https://localhost:44383/api/users/updatepassword',
                type: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(obj2),
                success: function (result) {
                    Swal.fire({ title: 'Success', 'text': ('Your password successfully updated'), 'type': 'success' })
                    console.log(result);
                    Swal.hideLoading();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    Swal.hideLoading();
                    Swal.fire({ title: 'Error', 'text': 'Your Password is Wrong', 'type': 'error' });
                }
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Swal.hideLoading();
            Swal.fire({ title: 'Error', 'text': 'Your Password is Wrong', 'type': 'error' });
        }
    });
   

}
$(document).ready(function () {
    $('#newpassword, #newpasswordverify').on('keyup', function () {
        if ($('#newpassword').val() == $('#newpasswordverify').val()) {
            $('#message').html('Password Matching').css('color', 'green');
        } else
            $('#message').html('Not Matching').css('color', 'red');
    });
});