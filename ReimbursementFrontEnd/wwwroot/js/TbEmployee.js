$(document).ready(function () {
    let nik2 = $("#nik2").val();
    $('#tableEmployee').DataTable({
        ajax: {
            url: 'https://localhost:44383/api/users/1',
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
                "data": null, "sortable": true,
                "render": function (data, type, row) {

                    var first = data.firstName;
                    var last = data.lastName;

                    return (first + " " + last);
                }
            },
            {
                "data": 'nik'

            },


            {
                "data": 'email'
            },
            {

                "data": 'gender'
            },
            {
                "data": 'managerNik'


            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn btn-info rounded-pill" data-toggle="modal" data-target="#viewModal"  onclick="Detail('${row['nik']}')" ><i class="fas fa-eye"></i></button>
                      `;
                }

            }

        ]
    });

});



function Detail(id) {
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44383/api/users/'+id,
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
        //console.log(id);
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
                    "render": function (data, type, row) {
                        return `
                      <input class="form-control rounded-pill" id="paidAmount" placeholder="${row['paidAmount']}" >
                     
                        `;
                    }
                }

            ]
        });

        $('#viewEmployee').DataTable().destroy();

    }).fail((error) => {
    })

}

function acc(id) {
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
        obj.status = "aprovedbymanager";
        obj.notes = result.notes;

        //obj.managerApprovalStatus = 1;
        obj.managerApprovalDate = new Date().toLocaleString();
        obj.financeApprovalStatus = result.financeApprovalStatus;
        obj.financeApprovalDate = result.financeApprovalDate;
        obj.nik = result.nik;
        //console.log(obj);
        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: 'https://localhost:44383/api/reimbursements/updatemanager/' + id + '/1/' + obj.status,

            type: "PUT",
            data: JSON.stringify(obj),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            beforeSend: function () {
                Swal.showLoading()
            },
        }).done((result) => {

            Swal.fire({ title: 'Success', 'text': ('Updated successfully'), 'type': 'success' })
            Swal.hideLoading();

            //$('#viewModal').modal('hide');
            $('#tableEmployee').DataTable().ajax.reload();

            //console.log(result);
            //$('#tableEmployee').DataTable().ajax.reload();

            //Swal.fire(
            //    'Success !',
            //    'Success Updated',
            //    'success'
            //)

        }).fail((error) => {

            Swal.hideLoading();
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            //console.log(data);
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


//function rej(id) {
//    $.ajax({
//        url: 'https://localhost:44383/api/reimbursements/' + id,
//        type: "GET",
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },

//    }).done((result) => {

//        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
//        obj.reimbursementId = id;
//        obj.requestDate = result.requestDate;
//        obj.status = "rejectedbymanager";
//        obj.notes = result.notes;

//        //obj.managerApprovalStatus = 2;
//        obj.managerApprovalDate = new Date().toLocaleString();
//        obj.financeApprovalStatus = 2;
//        obj.financeApprovalDate = result.financeApprovalDate;
//        obj.nik = result.nik;
//        //console.log(obj);
//        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
//        $.ajax({
//            url: 'https://localhost:44383/api/reimbursements/updatemanager/' + id + '/2/' + obj.status,

//            type: "PUT",
//            data: JSON.stringify(obj),
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },
//            beforeSend: function () {
//                Swal.showLoading()
//            },
//        }).done((result) => {
//            Swal.fire({ title: 'Success', 'text': ('Updated successfully'), 'type': 'success' })
//            Swal.hideLoading();
//            //console.log(result);
//            $('#tableEmployee').DataTable().ajax.reload();

//        }).fail((error) => {
//            Swal.hideLoading();
//            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
//            //console.log(data);
//        })
//    }).fail((error) => {
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