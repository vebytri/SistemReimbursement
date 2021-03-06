$(document).ready(function () {
    let nik2 = $("#nik2").val();
    $('#tableEmployee').DataTable({ 
        ajax: {
            url: 'https://localhost:44383/api/Reimbursements/getallbystatus/aprovedbymanager',

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
                    //console.log(data.account.user.firstName);

                    var first = data.account.user.firstName;
                    var last = data.account.user.lastName;

                    return (first + " " + last);
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
                            <button type="button" title="View Detail" class="btn btn-success rounded-pill" data-toggle="modal" data-target="#viewModal"  onclick="Detail('${row['reimbursementId']}')" ><i class="fas fa-check"></i></button>

                            <button type="button" title="Rejected" class="btn btn-danger rounded-pill" onclick="rej('${row['reimbursementId']}')"><i class="fas fa-times"></i></button>
                            `;
                }
                //< button type="button" class= "btn btn-success rounded-pill" onclick="acc('${row['reimbursementId']}')" > <i class="fas fa-check"></i></button>

            }

        ]
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
        $("#nik").val(result.nik);

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
            columnDefs: [
                { className: 'never', targets: 0 }
            ],
            responsive: true,
            columns: [
                {
                    "data": 'attachmentId'
                },
                {
                    "data": 'requestAmount'
                },
                {
                    "data": null, "sortable": true,
                    "render": function (data, type, row) {
                        return data.category.categoryName;
                    }
                },


                {
                    "data": 'fileAttachment',
                    "render": function (data, type, row) {
                        return `
                            ${data}
                            <button type="button" class="btn btn-info rounded-pill" title="Download" name="loop[]nameFile" onclick="Download('${row['fileAttachment']}')" ><i class="fas fa-download"></i></button>
                             `;
                    }

                },
                {
                    "render": function (data, type, row) {
                        return `
                      <input class="form-control rounded-pill" id="paidAmount" name="part[${row['attachmentId']}]paidAmount" placeholder="${row['paidAmount']}" >
                     
                        `;
                    }
                }
            ]


        });

        $('#viewEmployee').DataTable().destroy();

    }).fail((error) => {
    })

}
function Download(nameFile) {

    window.open("/Files/" + nameFile);

}


$('#submit').click(function (e) {
    e.preventDefault();
    var id = $("#reqId").val();
    var rd = $("#reqDate").val();
    var notes = $("#viewnotes").val();
    var ms = $("#managerStatus").val();
    var md = $("#managerDate").val();
    var nik = $("#nik").val();
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //  obj.reimbursementId = id;   
    //  obj.requestDate = new Date().toLocaleString();
    obj.status = "aprovedbyfinance";
    // obj.notes = notes;
    // obj.managerApprovalStatus = 1;
    //    obj.managerApprovalDate = new Date().toLocaleString();
    //  obj.financeApprovalStatus = 1;
    obj.financeApprovalDate = new Date().toLocaleString();
    //  obj.nik = nik;
    //  obj.financeApprovalNik = 1;
    //   console.log(obj)
    console.log("success");

    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/updatefinance/' + id + '/1/' + obj.status,
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
        Swal.fire({ title: 'Success', 'text': ('Your request successfully created'), 'type': 'success' })
        Swal.hideLoading();
        $('#viewModal').modal('hide');
        //console.log(result);
        $('#viewEmployee').DataTable().destroy();
        $('#tableEmployee').DataTable().ajax.reload();


    }).fail((error) => {
        Swal.hideLoading();
        Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
        console.log(data);

    })

    // $('#viewEmployee').DataTable({

    var obj2 = new Object();
    obj2.attachmentId = [];
    //obj2.requestAmount = [];
    //obj2.categoryId = [];
    //obj2.fileAttachment = [];
    obj2.paidAmount = [];


    //tabblecategory = ($('#viewEmployee').DataTable().columns().data()[2]);

    //for (j = 0; j < tabblecategory.length; j++) {
    //    if (tabblecategory[j] == "Transportation") {

    //    } else if (tabblecategory[j] == "Medical") {

    //    }
    //}
    var inputsrid = $('#viewEmployee').DataTable().columns().data()[0];
    //  console.log(inputsrid);
    //var inputsreq = document.querySelectorAll("#requestAmount");
    //var inputsrca = document.querySelectorAll("#category");
    //var inputsrup = document.querySelectorAll("#upload");
    var inputsrpd = document.querySelectorAll("#paidAmount");

    for (j = 0; j < inputsrid.length; j++) {
        obj2.attachmentId[j] = $('#viewEmployee').DataTable().columns().data()[0][j];
        console.log(obj2.attachmentId[j])
        obj2.paidAmount[j] = inputsrpd[j].value;
        $.ajax({
            url: 'https://localhost:44383/api/attachments/updatepaid/' + obj2.attachmentId[j] + '/' + obj2.paidAmount[j],
            type: "PUT",
            data: JSON.stringify(obj),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },

        }).done((result) => {
            //Swal.fire({ title: 'Success', 'text': ('Your request successfully created'), 'type': 'success' })
            //Swal.hideLoading();
            $('#viewModal').modal('hide');
            //console.log(result);
            $('#tableEmployee').DataTable().ajax.reload();
            //$('#viewModal').DataTable().fnClearTable();



        }).fail((error) => {
            Swal.hideLoading();
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            console.log(data);

        })
    }



    //$.ajax({
    //    url: 'https://localhost:44383/api/reimbursements',
    //    type: "PUT",
    //    data: JSON.stringify(obj),
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },

    //}).done((result) => {
    //    console.log(result);
    //    $('#tableEmployee').DataTable().ajax.reload();
    //    //$('#viewEmployee').dataTable().fnClearTable();


    //}).fail((error) => {


    //})

});

function updatePaid(id, paid1) {
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    var id2 = id;
    $.ajax({
        url: 'https://localhost:44383/api/attachments/' + id2,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        //$("#paidAmount").val(result.paidAmount);
        //var paid = document.querySelectorAll("#paidAmount");
        //var paid = $("#paidAmount").val();
        //console.log(paid1);


        var attachmentId = result.attachmentId;
        var reqAmount = result.requestAmount;;
        var category = result.categoryId;
        var attachment = result.fileAttachment;
        var reimb = result.reimbursementId;

        var obj = new Object();
        obj.attachmentId = attachmentId;
        obj.fileAttachment = attachment;
        obj.categoryId = category;
        obj.requestAmount = reqAmount;
        obj.paidAmount = paid;

        //obj.paidAmount = paid;
        obj.reimbursementId = reimb;
        console.log(obj);

        $.ajax({
            url: 'https://localhost:44383/api/attachments/',
            type: "PUT",
            data: JSON.stringify(obj),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        }).done((result) => {
            console.log(result);
            //$('#viewEmployee').dataTable().fnClearTable();

        }).fail((error) => {


        })

    }).fail((error) => {


    })

}

//function acc(id) {
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
//        obj.status = "aprovedbyfinance";
//        obj.notes = result.notes;

//        obj.managerApprovalStatus = result.managerApprovalStatus;
//        obj.managerApprovalDate = result.managerApprovalDate;
//        obj.financeApprovalStatus = 1;
//        obj.financeApprovalDate = new Date().toLocaleString();
//        obj.nik = result.nik;
//        obj.financeApprovalNik = result.financeApprovalNik;
//        console.log(obj);
//        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
//        $.ajax({
//            url: 'https://localhost:44383/api/reimbursements',
//            type: "PUT",
//            data: JSON.stringify(obj),
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },


//        }).done((result) => {
//            console.log(result);
//            $('#tableEmployee').DataTable().ajax.reload();

//        }).fail((error) => {


//        })

//    }).fail((error) => {


//    })

//    //$("#reqDate").val(result.requestDate.split("T")[0]);
//    //$("#status").val(result.status);
//    //$("#managerStatus").val(result.managerApprovalStatus);
//    //$("#managerDate").val(result.managerApprovalDate.split("T")[0]);
//    //$("#financeStatus").val(result.financeApprovalStatus);
//    //$("#financeDate").val(result.financeApprovalDate.split("T")[0]);
//    //$("#notes").val(result.notes);



//}
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
//        obj.status = "rejectedbyfinance";
//        obj.notes = result.notes;

//        obj.managerApprovalStatus = result.managerApprovalStatus;
//        obj.managerApprovalDate = result.managerApprovalDate;
//        obj.financeApprovalStatus = 2;
//        obj.financeApprovalDate = new Date().toLocaleString();
//        obj.nik = result.nik;
//        obj.financeApprovalNik = result.financeApprovalNik;
//        console.log(obj);
//        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
//        $.ajax({
//            url: 'https://localhost:44383/api/reimbursements',
//            type: "PUT",
//            data: JSON.stringify(obj),
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json'
//            },


//        }).done((result) => {
//            console.log(result);
//            $('#tableEmployee').DataTable().ajax.reload();

//        }).fail((error) => {


//        })

//    }).fail((error) => {


//    })

//    //$("#reqDate").val(result.requestDate.split("T")[0]);
//    //$("#status").val(result.status);
//    //$("#managerStatus").val(result.managerApprovalStatus);
//    //$("#managerDate").val(result.managerApprovalDate.split("T")[0]);
//    //$("#financeStatus").val(result.financeApprovalStatus);
//    //$("#financeDate").val(result.financeApprovalDate.split("T")[0]);
//    //$("#notes").val(result.notes);


//}


function rej(id) {
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
        //obj.requestDate = result.requestDate;
        obj.status = "rejectedbyfinance";
        //obj.notes = result.notes;

        //obj.managerApprovalStatus = 2;
        //obj.managerApprovalDate = new Date().toLocaleString();
        //obj.financeApprovalStatus = 2;
        //obj.financeApprovalDate = result.financeApprovalDate;
        //obj.nik = result.nik;
        //obj.financeApprovalNik = result.financeApprovalNik;
        //console.log(obj);
        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: 'https://localhost:44383/api/reimbursements/updatefinance2/' + id + '/2/' + obj.status,

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
            //console.log(result);
            $('#tableEmployee').DataTable().ajax.reload();

        }).fail((error) => {
            Swal.hideLoading();
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            //console.log(data);
        })
    }).fail((error) => {
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