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
                "sortable": true,
                "render": function (data, type, row) {

                   
                        var mnik = row.nik;
                        console.log(mnik);
                        var first = row.firstName;
                        var last = row.lastName;

                        var mname = first + " " + last;
                        console.log(mname);

                    if (row.managerNik) {

                    }
                    return `
                        <td> <select class="form-control rounded-pill" id="mnik">  <option value = "${mnik}"> ${mname}</option ></select ></td>
                       `;

                }
            },
          
          
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn btn-success rounded-pill" onclick="updatemnik('${row['nik']}')" ><i class="fas fa-check"></i></button>

                            `;
                }

            }

        ]
    });

});


function updatemnik(nik) {
    $.ajax({
        url: 'https://localhost:44383/api/users/' + nik,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {

        var nik1 = result.nik;
        var firstName = result.firstName;
        var lastName = result.lastName;
        var image = result.image;
        var managerNik = $("#mnik").val();
        var email = result.email;
        var birthDate = result.birthDate;
        var gender = result.gender;
        var address = result.address;

        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya

        obj.nik = nik1;
        obj.firstName = firstName;
        obj.lastName = firstName;
        obj.image = image;
        obj.managerNik = managerNik;
        obj.email = email;
        obj.birthDate = birthDate;
        obj.gender = gender;
        obj.address = address;

        $.ajax({
            url: 'https://localhost:44383/api/users/' + nik,
            type: "PUT",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(obj),

            beforeSend: function () {
                Swal.showLoading()
            },
        }).done((result) => {
            Swal.fire({ title: 'Success', 'text': ('Change successfully'), 'type': 'success' })
            Swal.hideLoading();

            $('#tableEmployee').DataTable().ajax.reload();

        }).fail((error) => {
            Swal.hideLoading();
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            console.log(data);

        })

}).fail((error) => {
   
})
    
  
}

