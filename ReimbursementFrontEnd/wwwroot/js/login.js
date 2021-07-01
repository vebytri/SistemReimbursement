////function cek() {
////    var email = $("#email").val();
////    var password = $("#password").val();
////    console.log(email);
////    console.log(password);
////}

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
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44383/api/users/login',
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)

    }).done((result) => {

        $("#email").val(result.email);
        $("#password").val(result.password);

        let timerInterval
        Swal.fire({
            title: 'Now Loading..',
            //html: 'I will close in <b></b> milliseconds.',
            timer: 1000,
            timerProgressBar: true,
            didOpen: () => {
                Swal.showLoading()
                
            },
          
        }).then((result) => {
            Swal.fire({
                icon: 'success',
                title: 'Login Success'   
            })
        })
        window.location = "/home";
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

