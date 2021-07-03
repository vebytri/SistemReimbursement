$(document).ready(function () {
    $('#password, #password2').on('keyup', function () {
        if ($('#password').val() == $('#password2').val()) {
            $('#message').html('Password Matching').css('color', 'green');
        } else
            $('#message').html('Not Matching').css('color', 'red');
    });
});
function register() {
    var first = $("#firstname").val();
    var last = $("#lastname").val();
    var gender = $("#gender").val();
    var date = $("#date").val();
    var address = $("#address").val();
    var role = $("#roleId").val();
    var mnik = $("#ManagerNik").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var password2 = $("#password2").val();


   

    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.FirstName = first;
    obj.LastName = last;

    obj.ManagerNik = mnik;
    obj.Email = email;
    obj.BirthDate = date;
    obj.Gender = gender;
    obj.Address = address;
    obj.Password = password;
    obj.RoleId = role;

    console.log(obj);

    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44383/api/users/register',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)

    }).done((result) => {
        console.log(result);  
        //alert("Data Sukses");
        Swal.fire({
            icon: 'success',
            title: 'Register Success'
        })
        window.location = "/login";

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            icon: 'error',
            title: 'Register Failed'
        })
        //alert("Data Gagal");
        console.log(error);
    });
}


////$(document).ready(function () {
//function isPasswordMatch() {
//    var password = $("#txtNewPassword").val();
//    var confirmPassword = $("#txtConfirmPassword").val();

//    if (password != confirmPassword) $("#divCheckPassword").html("Passwords do not match!");
//    else $("#divCheckPassword").html("Passwords match.");
//}

//$(document).ready(function () {
//    $("#txtConfirmPassword").keyup(isPasswordMatch);
//});

//function pass() {
//    $("#txtConfirmPassword").keyup(isPasswordMatch);
//    return a;
//}
////});
