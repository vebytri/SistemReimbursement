$(document).ready(function () {
    let nik2 = $("#nik2").val();
    $('#tableEmployee').DataTable({
        ajax: {
            url: 'https://localhost:44383/api/reimbursements/getallbynik/' + nik2,
            dataSrc: ''
        },
        columns: [
            {
                "data": null, "sortable": true,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }

            },
            {
                "data": 'reimbursementId'

            },
         
            {
                "data": 'status'
            },
            {
                
                 "data": null,
                "render": function (data, type, row)
                {
                    if (row.managerApprovalStatus == 1) {
                        return ("Aprroved");
                    }
                    else if (row.managerApprovalStatus == 0) {
                        return ("Process");

                    }
                    else if (row.managerApprovalStatus == 2) {
                        return ("Declined");

                    }
                    else
                    {
                        return ("Something Error");
                    }
                   
                }

            },
            {
                "data": null,
                "render": function (data, type, row) {
                    if (row.financeApprovalStatus == 1) {
                        return ("Aprroved");
                    }
                    else if (row.financeApprovalStatus == 0) {
                        return ("Process");

                    }
                    else if (row.financeApprovalStatus == 2) {
                        return ("Declined");

                    }
                    else {
                        return ("Something Error");
                    }
                }

            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn btn-info rounded-pill"  onclick="detail('${row['nik']}')" ><i class="fas fa-edit"></i></button>

                            <button type="button" class="btn btn-danger rounded-pill" onclick="del('${row['nik']}')"><i class="fas fa-trash"></i></button>
                            `;
                     }

            }

        ]
    });

});

//--add new data---

$(document).ready(function () {

    var i = 1;

    $("#add").click(function () {
        i++;
        $('#dynamic_field').append('<tr id="row' + i + '"> <td><input type="text" name="requestAmount[' + i + ']" placeholder="Request Amount" class="form-control rounded-pill" /></td><td> <select class="form-control rounded-pill" id="category[' + i + ']">  < option value = "1" > Medical</option > <option value="2">Transportation</option></select ></td > <td><input type="file" name="upload[' + i + ']" placeholder="Upload File" class="form-control-file " /></td> <td><button type="button" name="remove" id="' + i + '" class="btn btn-danger rounded-pill btn_remove">X</button></td></tr>');
    });

    $(document).on('click', '.btn_remove', function () {
        var button_id = $(this).attr("id");
        $('#row' + button_id + '').remove();
    });


    $('#submit').click(function () {
        var nik = $("#nik2").val();
        var requestDate = Date.now;
        var status = "Process";
        console.log(i);

        //$('input[name="requestAmount[]"]').each(function (index) {
        //    //alert(index);
        //    console.log(index);
        //});

        //var requestAmount1 = document.querySelector('#formID');
        //var requestAmount1 = document.getElementsByName('requestAmount');
        //var requestAmount1 = document.getElementsById('#part[]');

        var fd = new FormData(this);
        console.log(fd);

        //var requestAmount = $(this).requestAmount1;

        console.log(requestAmount1);

        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        obj.requestDate = requestDate;
        obj.status = status;
        obj.notes = $("#notes").val();
        obj.nik = nik;
       

        for (var j = 0; j < i; j++) {

            obj[i].requestAmount = $("#requestAmount").val();
            obj[i].fileAttachment = $("#upload").val();
            obj[i].categoryId = $("#category").val();
        }

        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: 'https://localhost:44383/api/accounts/request/' + i,
            type: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj)

        }).done((result) => {

            $('#tableEmployee').DataTable().ajax.reload();
            //buat alert pemberitahuan jika success
            //alert("Data Sukses");
            Swal.fire(
                'Success !',
                'Data Berhasil Di Tambahkan',
                'success'
            )
            console.log(result);
        }).fail((error) => {
            //alert pemberitahuan jika gagal

            Swal.fire(
                'Failed !',
                'Data Gagal di Tambahkan',
                'error'
            )
            //alert("Data Gagal");
            console.log(error);
        });
    });
});
//function insertRequest() {
//    var nik = $("#nik2").val();

//    var req = $("#requestAmount");

//    var requestDate = Date.now;
//    var status = "Process";
//    console.log(req);

//    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
//    for (var i = 0; i < req.length; i++) {

//        obj.requestDate = requestDate;
//        obj.status = status;
//        obj.notes = $("#notes").val();
//        obj.nik = nik;
//        obj[i].requestAmount= $("#requestAmount").val();
//        obj[i].fileAttachment = $("#upload").val();
//        obj[i].categoryId = $("#category").val();
//    }
    
//    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
//    $.ajax({
//        url: 'https://localhost:44383/api/accounts/request/1' ,
//        type: "POST",
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'      
//        },
//        data: JSON.stringify(obj)

//    }).done((result) => {

//        $('#tableEmployee').DataTable().ajax.reload();
//        //buat alert pemberitahuan jika success
//        //alert("Data Sukses");
//        Swal.fire(
//            'Success !',
//            'Data Berhasil Di Tambahkan',
//            'success'
//        )
//        console.log(result);
//    }).fail((error) => {
//        //alert pemberitahuan jika gagal

//        Swal.fire(
//            'Failed !',
//            'Data Gagal di Tambahkan',
//            'error'
//        )  
//        //alert("Data Gagal");
//        console.log(error);
//    })

//}


function updateProfile() {

    bootstrapValidate('#firstname', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#lastname', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#phone', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#birthdate', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#salary', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#password', 'min:8:password minimal 8 karakter');
    //bootstrapValidate('#email', 'email: masukan email yang valid dan belum pernah di pakai');
    bootstrapValidate('#degree', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#gpa', 'required:Kolom tidak boleh kosong');
    bootstrapValidate('#universityid', 'required:Kolom tidak boleh kosong');

    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#firstname").val();
    obj.LastName = $("#lastname").val();
    obj.Phone = $("#phone").val();
    obj.BirthDate = $("#birthdate").val();
    obj.Salary = $("#salary").val();
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val();
    obj.UniversityId = $("#universityid").val();


    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44345/API/person/UpdateProfile',
        type: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)
    }).done((result) => {
        $('#tableProf').DataTable().ajax.reload();
        //buat alert pemberitahuan jika success
        //alert("Data Sukses");
        Swal.fire(
            'Success !',
            'Data Berhasil Di Ubah',
            'success'
        )
        //console.log(result);
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire(
            'Failed !',
            'Data Gagal di Ubah',
            'error'
        )
        bootstrapValidate('#email', 'email: email sudah digunakan');

        //alert("Data Gagal");
        console.log(error);
    })
    //console.log(obj);
}

function del(stringUrl) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'https://localhost:44345/API/person/DeleteProfilbyId/' + stringUrl,
                type: "POST"
            }).done((result) => {
                //console.log(result);
                $('#tableProf').DataTable().ajax.reload();
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            }).fail((error) => {
                Swal.fire(
                    'Failed !',
                    'Data Gagal di Hapus',
                    'error'
                )
                console.log(error);
            });

        }
    })


}






//--view pdf--
// Loaded via <script> tag, create shortcut to access PDF.js exports.
var pdfjsLib = window['pdfjs-dist/build/pdf'];
// The workerSrc property shall be specified.
pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://mozilla.github.io/pdf.js/build/pdf.worker.js';

$("#myPdf").on("change", function (e) {
    var file = e.target.files[0]
    if (file.type == "application/pdf") {
        var fileReader = new FileReader();
        fileReader.onload = function () {
            var pdfData = new Uint8Array(this.result);
            // Using DocumentInitParameters object to load binary data.
            var loadingTask = pdfjsLib.getDocument({ data: pdfData });
            loadingTask.promise.then(function (pdf) {
                console.log('PDF loaded');

                // Fetch the first page
                var pageNumber = 1;
                pdf.getPage(pageNumber).then(function (page) {
                    console.log('Page loaded');

                    var desiredWidth = 350;
                    var viewport = page.getViewport({ scale: 1, });
                    var scale = desiredWidth / viewport.width;
                    /*var scale = 1.5;*/
                    var viewport = page.getViewport({ scale: scale });

                    // Prepare canvas using PDF page dimensions
                    var canvas = $("#pdfViewer")[0];
                    var context = canvas.getContext('2d');
                    canvas.height = viewport.height;
                    canvas.width = viewport.width;

                    // Render PDF page into canvas context
                    var renderContext = {
                        canvasContext: context,
                        viewport: viewport
                    };
                    var renderTask = page.render(renderContext);
                    renderTask.promise.then(function () {
                        console.log('Page rendered');
                    });
                });
            }, function (reason) {
                // PDF loading error
                console.error(reason);
            });
        };
        fileReader.readAsArrayBuffer(file);
    }
});