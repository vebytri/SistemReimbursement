$(document).ready(function () {
    //let nik2 = $("#nik2").val();
    //let first = $("#first").val();
    //let last = $("#last").val();
    $('#tableEmployee').DataTable({
        ajax: {
            url: 'https://localhost:44383/api/users/',
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
                "data": 'gender'
            },
            {
                "data": 'email'
            },
            {
                "data": null, "sortable": true,
                "render": function (data, type, row) {
                    var date = data.birthDate.split("T")[0];
                    return (date);
                }
            },
            {
                
                "data": 'managerNik', "sortable": true,
                "render": function (data, type, row) {
                    if (data == 0) {
                        return ("not available");
                    }
                    else {
                        return data;
                    }
                }

                
            
            },
          
          
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn btn-info rounded-pill" data-toggle="modal" data-target="#viewModal" title="View Detail" onclick="Detail('${row['nik']}')" ><i class="fas fa-eye"></i></button>

                            `;
                }

            }

        ]
    });

});
function Detail(id) {
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: 'https://localhost:44383/api/users/' + id,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        $("#nik").val(result.nik);
        $("#birthdate").val(result.birthDate.split("T")[0]);
       var first=result.firstName;
       var last= result.lastName;
        $("#fullname").val(first +' '+ last);
    }).fail((error) => {


    })

}



function updatemnik() {
   var nik2= $("#nik").val();

    $.ajax({
        url: 'https://localhost:44383/api/users/' + nik2,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {

        var nik1 = result.nik;
        var managerNik = $("#managernik").val();
        console.log($("#roles").val())
        console.log($("#managernik").val())
        console.log(result.nik)

        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya

        obj.Nik = nik1;
        obj.ManagerNik = managerNik;
        var obj2 = new Object();
        obj2.Nik = obj.Nik;
        obj2.RoleId = $("#roles").val();

        if ($("#roles").val() == null && $("#managernik").val() != null) {
            console.log("role kosong")
                $.ajax({
                    url: 'https://localhost:44383/api/users/updatemnik/',
                    type: "POST",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(obj),

                    beforeSend: function () {
                        Swal.showLoading()
                    },
                }).done((result) => {

                    Swal.fire({ title: 'Success', 'text': ('You changed user Manager!!'), 'type': 'success' })
                    console.log(result);
                    Swal.hideLoading();
                    $('#viewModal').modal('hide');
                    $('#tableEmployee').DataTable().ajax.reload();

                }).fail((error) => {
                    Swal.hideLoading();
                    Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
                    console.log(data);

                })
        }
        if ($("#managernik").val() == null && $("#roles").val() != null) {
        console.log("managernik kosong")
            $.ajax({
                url: 'https://localhost:44383/api/users/updaterole/' + obj2.Nik + '/' + obj2.RoleId,
                type: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json' 
                },
                data: JSON.stringify(obj2),
                success: function (result) {
                    Swal.fire({ title: 'Success', 'text': ('You changed user data!!'), 'type': 'success' })
                    console.log(result);
                    Swal.hideLoading();
                    $('#viewModal').modal('hide');
                    $('#tableEmployee').DataTable().ajax.reload();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    Swal.hideLoading();
                    Swal.fire({ title: 'Error', 'text': 'Error Encountered when updating data', 'type': 'error' });
                    $('#viewModal').modal('hide');
                    $('#tableEmployee').DataTable().ajax.reload();
                }
            });
        }
        if ($("#managernik").val() != null && $("#roles").val() != null) {
            $.ajax({
                url: 'https://localhost:44383/api/users/updatemnik/',
                type: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(obj),

                beforeSend: function () {
                    Swal.showLoading()
                },
            }).done((result) => {
                $.ajax({
                    url: 'https://localhost:44383/api/users/updaterole/' + obj2.Nik + '/' + obj2.RoleId,
                    type: "POST",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(obj2),
                    success: function (result) {
                        Swal.fire({ title: 'Success', 'text': ('You changed user data!!'), 'type': 'success' })
                        console.log(result);
                        Swal.hideLoading();
                        $('#viewModal').modal('hide');
                        $('#tableEmployee').DataTable().ajax.reload();
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        Swal.hideLoading();
                        Swal.fire({ title: 'Error', 'text': 'Error Encountered when updating data', 'type': 'error' });
                        $('#viewModal').modal('hide');
                        $('#tableEmployee').DataTable().ajax.reload();
                    }
                });


            }).fail((error) => {
                Swal.hideLoading();
                Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
                console.log(data);

            })
        }

}).fail((error) => {
   
})
    
  
}

