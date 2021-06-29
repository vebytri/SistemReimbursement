function cek() {
    var email = $("#email").val();
    var password = $("#password").val();

    console.log(email);
    console.log(password);
}
//function cek() {
//    var firstname = $("#firstname").val();
//    var lastname = $("#lastname").val();
//    var gender = $("#gender").val();
//    var date = $("#date").val();
//    var job = $("#job").val();
//    var address = $("#address").val();
//    var email = $("#email").val();
//    var password = $("#password").val();

//    console.log(firstname);
//    console.log(lastname);
//    console.log(gender);
//    console.log(date);
//    console.log(email);
//    console.log(job);
//    console.log(address);
//    console.log(password);


//}
function login() {
  
    //bootstrapValidate('#firstname', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#lastname', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#phone', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#birthdate', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#salary', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#email', 'email: email sudah digunakan');
    //bootstrapValidate('#password', 'min:8:password minimal 8 karakter');
    //bootstrapValidate('#degree', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#gpa', 'required:Kolom tidak boleh kosong');
    //bootstrapValidate('#universityid', 'required:Kolom tidak boleh kosong');

    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.email = $("#email").val();
    obj.password = $("#password").val();

   


    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: '/login/Auth',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            //'success': function (e) {
            //    if (e.some(e.email == null)) {
            //    }
            //}
        },
        data: JSON.stringify(obj)

    }).done((result) => {

        $("#email").val(result.email);
        $("#password").val(result.password);

        window.location.href = "/home";
        console.log(result);

        
    }).fail((error) => {
        //alert pemberitahuan jika gagal

        Swal.fire(
            'Failed !',
            'Login Gagal',
            'error'
        )
      
        //alert("Data Gagal");
        console.log(error);
    })

}

