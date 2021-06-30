﻿$(document).ready(function () {
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
                "render": function (data, type, row) {
                    if (row.managerApprovalStatus == 1) {
                        return ("Aprroved");
                    }
                    else if (row.managerApprovalStatus == 0) {
                        return ("Process");

                    }
                    else if (row.managerApprovalStatus == 2) {
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
                    if (row.financeApprovalStatus == 1) {
                        return ("Aprroved");
                    }
                    else if (row.financeApprovalStatus == 0) {
                        return ("Process");

                    }
                    else if (row.financeApprovalStatus == 2) {
                        return ("Rejected");

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
                            <button type="button" class="btn btn-info rounded-pill" data-toggle="modal" data-target="#viewModal"  onclick="Detail('${row['reimbursementId']}')" ><i class="fas fa-eye"></i></button>

                            <button type="button" class="btn btn-danger rounded-pill" onclick="del('${row['reimbursementId']}')"><i class="fas fa-trash"></i></button>
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
        $('#dynamic_field').append('<tr id="row' + i + '"> <td><input type="text" id="requestAmount" name=""loop[]req" placeholder="Request Amount" class="form-control rounded-pill" /> </td ><td> <select class="form-control rounded-pill" id="category" name="loop[]type">  < option value = "1" > Medical</option > <option value="2">Transportation</option></select ></td > <td><input type="file" id="upload" name="loop[]file" placeholder="Upload File" class="form-control-file " /></td> <td><button type="button" name="remove" id="' + i + '" class="btn btn-danger rounded-pill btn_remove">X</button></td></tr > ');

        console.log(show_value);
    });

    $(document).on('click', '.btn_remove', function () {
        var button_id = $(this).attr("id");
        $('#row' + button_id + '').remove();
    });


    $('#submit').click(function () {
        var nik = $("#nik2").val();
        var mnik = $("#mnik").val();
        console.log(mnik);
        var requestDate = new Date().toLocaleString();
        var status = "Process";
        var notes = $("#notes").val();
        console.log(notes);

        var inputsreq = document.querySelectorAll("#requestAmount");
        var inputsup = document.querySelectorAll("#upload");
        var inputscat = document.querySelectorAll("#category");

        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        obj.requestDate = requestDate;
        obj.status = status;
        obj.notes = notes;
  

        obj.nik = nik;
        obj.FinanceApprovalNik = mnik;


        obj.requestAmount = [];
        obj.fileAttachment = [];
        obj.categoryId = [];

        // for (j = 0; j < inputsreq.length; j++) {
        //    console.log(inputsreq[j].value);
        //    console.log(inputsup[j].value);
        //    console.log(inputscat[j].value);
        //}
        for (var j = 0; j < i; j++) {
            obj.requestAmount[j] = inputsreq[j].value;
            obj.fileAttachment[j] = inputsup[j].value;
            obj.categoryId[j] = inputscat[j].value;
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
            console.log(result);

            $('#tableEmployee').DataTable().ajax.reload();
            //buat alert pemberitahuan jika success
            //alert("Data Sukses");
            Swal.fire(
                'Success !',
                'Data Berhasil Di Tambahkan',
                'success'
            )
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


function Detail(id) {
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/' + id,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        $("#reqId").val(result.reimbursementId);

        $("#reqDate").val(result.requestDate.split("T")[0]);
        $("#status").val(result.status);

        if (result.managerApprovalStatus == 0) {
            $("#managerStatus").val("Process");
        }
        else if (result.managerApprovalStatus == 1) {
            $("#managerStatus").val("Approved");
        }
        else if (result.managerApprovalStatus == 2) {
            $("#managerStatus").val("Rejected");
        }
        else {
            $("#managerStatus").val("Unknown!");
        }
   

        
       
     
        if (result.managerApprovalDate == "0001-01-01T00:00:00") {
            $("#managerDate").val("Not Available");
        }

        else {
            $("#managerDate").val(result.managerApprovalDate.split("T")[0]);
        }


        if (result.financeApprovalStatus == 0) {
            $("#financeStatus").val("Process");
        }
        else if (result.financeApprovalStatus == 1) {
            $("#financeStatus").val("Approved");
        }
        else if (result.financeApprovalStatus == 2) {
            $("#financeStatus").val("Rejected");
        }
        else {
            $("#financeStatus").val("Unknown!");

        }

        if (result.financeApprovalDate == "0001-01-01T00:00:00") {
            $("#financeDate").val("Not Available");
        }

        else {
            $("#financeDate").val(result.financeApprovalDate.split("T")[0]);

        }
        $("#viewnotes").val(result.notes);


        //---tablemodal--
      
            console.log(id);
            $('#viewEmployee').DataTable({
                ajax: {
                    url: 'https://localhost:44383/api/attachments/getdetail/' + id,
                    dataSrc: ''
                },
                columns: [

                    {
                        "data": 'requestAmount'

                    },

                    {
                        "data": 'categoryId'

                    },

                    {
                        "data": 'fileAttachment'

                    },
                    {
                        "data": 'paidAmount'

                    }

                ]
            });
        $('#viewEmployee').DataTable().destroy();

       



    }).fail((error) => {


    })

}

function del(id) {
    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/' + id,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
      
        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        obj.reimbursementId = id;
        obj.requestDate = result.requestDate;
        obj.status = "Deleted";
        obj.notes = result.notes;

        obj.managerApprovalStatus = result.managerApprovalStatus;
        obj.managerApprovalDate = result.managerApprovalDate;
        obj.financeApprovalStatus = result.financeApprovalStatus;
        obj.financeApprovalDate = result.financeApprovalDate;
        obj.nik = result.nik;
        obj.financeApprovalNik = result.financeApprovalNik;
        console.log(obj);
        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: 'https://localhost:44383/api/reimbursements',
            type: "PUT",
            data: JSON.stringify(obj),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },


        }).done((result) => {
            console.log(result);
            $('#tableEmployee').DataTable().ajax.reload();

        }).fail((error) => {


        })

    }).fail((error) => {


    })

    //$("#reqDate").val(result.requestDate.split("T")[0]);
    //$("#status").val(result.status);
    //$("#managerStatus").val(result.managerApprovalStatus);
    //$("#managerDate").val(result.managerApprovalDate.split("T")[0]);
    //$("#financeStatus").val(result.financeApprovalStatus);
    //$("#financeDate").val(result.financeApprovalDate.split("T")[0]);
    //$("#notes").val(result.notes);


}


//function del(stringUrl) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: 'https://localhost:44345/API/person/DeleteProfilbyId/' + stringUrl,
//                type: "POST"
//            }).done((result) => {
//                //console.log(result);
//                $('#tableProf').DataTable().ajax.reload();
//                Swal.fire(
//                    'Deleted!',
//                    'Your file has been deleted.',
//                    'success'
//                )
//            }).fail((error) => {
//                Swal.fire(
//                    'Failed !',
//                    'Data Gagal di Hapus',
//                    'error'
//                )
//                console.log(error);
//            });

//        }
//    })


//}






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