$(document).ready(function () {
    $('#tableEmployee').DataTable({
        ajax: {
            url: 'https://localhost:44383/api/reimbursements/getallbynik/2',
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
                "data": 'requestAmount'
            },
            {
                "data": 'paidAmount'
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
                            <button type="button" class="btn btn-primary" onclick="detail('${row['nik']}')" data-toggle="modal" data-target="#exampleModalCenter">Detail</button>

                            <button type="button" class="btn btn-danger" onclick="del('${row['nik']}')" >Delete</button>
                            `;
                }

            }

        ]
    });

});