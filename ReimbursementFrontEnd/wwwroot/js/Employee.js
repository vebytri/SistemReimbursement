$(document).ready(function () {
    $('#tableEmployee').DataTable({
        ajax: {
            url: '/user/getsemuadata',
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
                "data": '"reimbursementId": '

            },
            {
                "data": 'firstName'

            },
            {
                "data": 'requestAmount'
            },
            {
                "data": 'paidAmount'
            },
            {
                "data": 'categoryId'
            },
            {
                "data": 'status'
            },
            {
                "data": 'managerApprovalStatus'
            },
            {
                "data": 'financeApprovalStatus'
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